namespace ControllRR.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile form, string folder);
}