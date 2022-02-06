using Cms.Shared;

namespace Cms.ContentTemplate
{
    public class SaveTemplateUOW : ISaveTemplateUOW
    {
        private readonly IQueryableRepos<ContentTemplateAggregate, int> _templateRepos;
        private readonly IQueryableRepos<ContentField, int> _fieldRepos;

        public SaveTemplateUOW(IQueryableRepos<ContentTemplateAggregate,int> templateRepos, IQueryableRepos<ContentField,int> fieldRepos)
        {
            _templateRepos = templateRepos;
            _fieldRepos = fieldRepos;
        }
        public async Task<ContentTemplateAggregate> DoWork(ContentTemplateAggregate entity)
        {
            using var connection = _templateRepos.GetConnection();
            await connection.OpenAsync();
            using var trans = await _templateRepos.WithTransaction();
            _fieldRepos.With(connection, trans);

            var templateEntity = await _templateRepos.Create(entity);
            foreach(var field in entity.ContentFields)
            {
                field.ContentTemplateId = templateEntity.Id;
                var fieldEntity = await _fieldRepos.Create(field);
                field.Id = fieldEntity.Id;
            }
            entity.Id = templateEntity.Id;
            trans.Commit();
            return entity;
        }
    }
}
