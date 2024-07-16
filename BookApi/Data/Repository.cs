using BookApi.Models;
using System.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookApi.Data
{
    public class Repository
    {
        private readonly string _connectionString;
        public Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public BookModel GetBookById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new BookModel
                        {
                            id = reader.GetInt32(0),
                            title = reader.GetString(1),
                            author = reader.GetString(2),
                            price = reader.GetDecimal(3)
                        };
                    }
                }
            }
            return null;
        }
        public IEnumerable<BookModel> GetBooks()
        {
            var books = new List<BookModel>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Books", conn);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new BookModel
                        {
                            id = reader.GetInt32(0),
                            title = reader.GetString(1),
                            author = reader.GetString(2),
                            price = reader.GetDecimal(3)
                        });
                    }
                }
            }
            return books;
        }
        public void AddBook(BookModel book)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Books (title, author, price) values(@title, @author, @price)", conn);
                cmd.Parameters.AddWithValue("@Title", book.title);
                cmd.Parameters.AddWithValue("@Author", book.author);
                cmd.Parameters.AddWithValue("@Price", book.price);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateBook(BookModel book)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Books SET title = @Title, author = @Author, price = @Price WHERE id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", book.id);
                cmd.Parameters.AddWithValue("@Title", book.title);
                cmd.Parameters.AddWithValue("@Author", book.author);
                cmd.Parameters.AddWithValue("@Price", book.price);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteBook(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
