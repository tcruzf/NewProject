using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IDocumentRepository
{

    Task<List<Document>> ListAllAsync();
    Task InsertAsync(Document document);
     Task SaveChangesAsync();
}