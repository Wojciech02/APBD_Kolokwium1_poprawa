using KOLOKWIUM.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace KOLOKWIUM.Repositorys
{
    public class Repository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<LibraryDto> GetLibraryAsync(int id)
        {
            LibraryDto library = null;
            List<BookDto> books = new List<BookDto>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT l.Id, l.Name, l.Address, b.Id, b.Title, b.PublicationDate, a.Name AS AuthorName, c.Name AS CategoryName " +
                    "FROM Libraries l " +
                    "LEFT JOIN Books b ON l.Id = b.LibraryId " +
                    "LEFT JOIN Authors a ON b.AuthorId = a.Id " +
                    "LEFT JOIN Categories c ON b.CategoryId = c.Id " +
                    "WHERE l.Id = @Id " +
                    "ORDER BY b.PublicationDate DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (library == null)
                            {
                                library = new LibraryDto
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Address = reader.GetString(2),
                                    Books = books
                                };
                            }

                            if (!reader.IsDBNull(3))
                            {
                                books.Add(new BookDto
                                {
                                    Id = reader.GetInt32(3),
                                    Title = reader.GetString(4),
                                    PublicationDate = reader.GetDateTime(5),
                                    AuthorName = reader.GetString(6),
                                    CategoryName = reader.GetString(7)
                                });
                            }
                        }
                    }
                }
            }

            return library;
        }
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE AuthorId = @Id", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Authors WHERE Id = @Id", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            int rowsAffected = await cmd.ExecuteNonQueryAsync();

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
    
}
