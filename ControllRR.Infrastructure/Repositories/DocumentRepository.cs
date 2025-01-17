using ControllRR.Infrastructure.Data.Context;

using ControllRR.Domain.Entities;

//using ControlRR.Services.Exceptions;
//using ControlRR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;

public class DocumentRepository : IDocumentRepository
{
    private readonly ControllRRContext _controllRRContext;

    public DocumentRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }
    public async Task<List<Document>> ListAllAsync()
    {
      var obj = await _controllRRContext.Documents.ToListAsync();
      return obj;
    }

    public async Task InsertAsync(Document document)
    {
      
         _controllRRContext.Documents.Add(document);
         await _controllRRContext.SaveChangesAsync();

    }

      public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }


}