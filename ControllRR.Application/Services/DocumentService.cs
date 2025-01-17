
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }
    public async Task<List<Document>> ListAllAsync()
    {
     return await _documentRepository.ListAllAsync();
    }

    public async Task InsertAsync(Document document)
    {
      
      throw new NotImplementedException();
      
    }


}