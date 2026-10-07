using Microsoft.EntityFrameworkCore;
using Bibliotheque_Publique_Projekt.Models;

namespace Bibliotheque_Publique_Projekt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("NextJsPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:5173/") // Next.js standard adresse
                        .AllowAnyMethod()                       // Tillad GET, POST, PUT, DELETE
                        .AllowAnyHeader();                      // Tillad alle headers (f.eks. JSON-kald)
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddAuthentication();

            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<BooksContext>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<TodoDB>(options =>

            //    options.UseSqlite("Data Source=Todo.db")

            //);

            var app = builder.Build();

            app.UseCors("NextJsPolicy");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            /* ---------------------------------------------------------------------------------------------------------------------- */

            //GET

            app.MapGet("/getBooksByGenre/{genreId}", async (int genreId, BooksContext db) =>

            {

                var books = await db.Books

                    .Where(b => b.GenreId == genreId)

                    .ToListAsync();


                return Results.Ok(books);

            });

            /* ---------------------------------------------------------------------------------------------------------------------- */

            //GET ()

            app.MapGet("/getBooksById/{Id}", async (int id, BooksContext db) =>

            {

                var books = await db.Books

                    .Where(b => b.Id == id)

                    .ToListAsync();


                return Results.Ok(books);

            });

            /* ---------------------------------------------------------------------------------------------------------------------- */

            //POST

            //app.MapPost("/createBook/{genreId}", async (int genreId, Book books, BooksContext db) =>
            //{

            //    db.Books.Add(books);
            //    await db.SaveChangesAsync();

            //    return Results.Created($"/createBook/{genreId}", books);

            //});

            /* ---------------------------------------------------------------------------------------------------------------------- */

            //PUT

            //app.MapPut("/getBooksById/{Id}", async (int id, Book inputBook, BooksContext db) =>

            //{

            //    var books = await db.Books.FindAsync(id);

            //    if (books == null) return Results.NotFound();

            //    books.Title = inputBook.Title;
            //    books.Description = inputBook.Description;
            //    books.GenreId = inputBook.GenreId;

            //    await db.SaveChangesAsync();


            //    return Results.Ok($"Book med ID {id} er opdateret.");

            //});

            /* ---------------------------------------------------------------------------------------------------------------------- */

            //DELETE

            //app.MapDelete("/getBooksbyId/{Id}", async (int id, BooksContext db) =>
            //{

            //    if (await db.Books.FindAsync(id) is Book books)
            //    {
            //        db.Books.Remove(books);
            //        await db.SaveChangesAsync();

            //        return Results.Ok($"Book med ID {id} er opdateret.");
            //    }

            //    return Results.NotFound();

            //});

            /* ---------------------------------------------------------------------------------------------------------------------- */

            app.Run();
        }
    }
}
