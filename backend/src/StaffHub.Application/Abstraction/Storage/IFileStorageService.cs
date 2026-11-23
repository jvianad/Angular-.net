namespace StaffHub.Application.Abstractions.Storage;

public interface IFileStorageService
{
    Task<string> UploadAsync(
        Stream fileStream,
        string fileName,
        string folder,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string? fileUrl, CancellationToken cancellationToken = default);
}