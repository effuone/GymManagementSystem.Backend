using Microsoft.AspNetCore.Http;

namespace GMS.Core.Services
{
    public static class FileService
    {
        public static async Task<string> SaveFileAsync(IFormFile uploadFile, string env, string folder)
        {
            using (var memoryStream = new MemoryStream())
            {
                try{
                     await uploadFile.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        if(!Directory.Exists(env + $"{folder}"))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        string fileName = uploadFile.FileName;
                        string physicalPath = env + @$"{folder}/" + fileName;
                        using(var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            await uploadFile.CopyToAsync(stream);
                            using(var ms = new MemoryStream())
                            {
                                await uploadFile.CopyToAsync(ms);
                            }
                        }
                        return env + @$"{folder}/" + uploadFile.FileName;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return "";
        }
    }
}