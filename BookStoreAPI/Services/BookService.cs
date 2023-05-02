using BookStoreAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreAPI.Services;

public class BookService
{
    private readonly IMongoCollection<Book> _bookCollection;

    public BookService(IOptions<BookStoreDatabaseSetting> bookStoreDatabaseSetting)
    {
        var mongoClient = new MongoClient(bookStoreDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSetting.Value.DatabaseName);

        _bookCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSetting.Value.BookCollectionName);
    }

    public async Task<List<Book>> GetAsync() => await _bookCollection.Find(_ => true).ToListAsync();

    public async Task<Book?> GetAsync(string id) => await _bookCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book newBook) => await _bookCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Book updateBook) => await _bookCollection.ReplaceOneAsync(x => x.Id == id, updateBook);

    public async Task RemoveAsync(string id) => await _bookCollection.DeleteOneAsync(x => x.Id == id);
}