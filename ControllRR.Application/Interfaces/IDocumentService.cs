using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;

public interface IDocumentService
{

    Task<List<Document>> ListAllAsync();
    Task InsertAsync(Document document);
}