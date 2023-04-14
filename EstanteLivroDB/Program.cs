using EstanteLivroDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var dataBase = client.GetDatabase("Bookcase");
        var collectionBook = dataBase.GetCollection<BsonDocument>("Book");
        var collectionLoan = dataBase.GetCollection<BsonDocument>("Loan");
        var collectionReading = dataBase.GetCollection<BsonDocument>("Reading");

        UserInterface(collectionBook, collectionLoan, collectionReading);

    }

    #region Menu Interaco Usuario
    private static void UserInterface(IMongoCollection<BsonDocument> collectionBook, IMongoCollection<BsonDocument> collectionLoan, IMongoCollection<BsonDocument> collectionReading)
    {
        do
        {
            switch (BookcaseMenu())
            {
                case 1:
                    Console.Clear();

                    RegisterBook(collectionBook);

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.Clear();

                    Console.WriteLine("Informe o título do livro: ");

                    UserEditInterface(collectionBook, Console.ReadLine());

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.Clear();

                    ShowBookcase(collectionBook);

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.Clear();

                    Console.WriteLine("Informe o título do livro: ");
                    string findTitleDelete = Console.ReadLine();

                    Console.WriteLine("Você tem certeza que deseja EXCLUIR este livro? [S]im | [N]ão");
                    if (VerifyChar() == 's')
                    {
                        DeleteBook(collectionBook, findTitleDelete);
                    }

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.Clear();

                    Console.WriteLine("Informe o título do livro: ");

                    ShowBookByTitle(collectionBook, Console.ReadLine());

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 6:
                    Console.Clear();

                    var documentCount = collectionReading.Find(new BsonDocument()).CountDocuments();

                    if (documentCount <= 1)
                    {
                        Console.WriteLine("Informe o título do livro: ");
                        string findTitleReading = Console.ReadLine();

                        Console.WriteLine("Você tem certeza que deseja LER este livro? [S]im | [N]ão");
                        if (VerifyChar() == 's')
                        {
                            InsertBook(collectionBook, collectionReading, findTitleReading);
                            DeleteBook(collectionBook, findTitleReading);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Você já tem muitos livros para ler.\nOpte por terminá-los antes");
                    }

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 7:
                    Console.Clear();

                    Console.WriteLine("Informe o título do livro: ");
                    string findTitleLoan = Console.ReadLine();

                    Console.WriteLine("Você tem certeza que deseja EMPRESTAR este livro? [S]im | [N]ão");
                    if (VerifyChar() == 's')
                    {
                        InsertBook(collectionBook, collectionLoan, findTitleLoan);
                        DeleteBook(collectionBook, findTitleLoan);
                    }

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 8:
                    Console.Clear();

                    Console.WriteLine("Informe o título do livro: ");
                    string findTitleFinished = Console.ReadLine();
                    InsertBook(collectionReading, collectionBook, findTitleFinished);
                    DeleteBook(collectionReading, findTitleFinished);

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 9:
                    Console.Clear();

                    Console.WriteLine("Informe o título do livro: ");
                    string findTitleReturned = Console.ReadLine();
                    InsertBook(collectionLoan, collectionBook, findTitleReturned);
                    DeleteBook(collectionLoan, findTitleReturned);

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 10:
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida");
                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
            }
        } while (true);
    }

    private static int BookcaseMenu()
    {
        Console.Clear();
        Console.WriteLine(">>>Menu de opções<<<\n\n1 - Adiciona Livro\n2 - Editar Livro\n" +
            "3 - Mostrar Estante\n4 - Deletar Livro\n5 - Buscar Livro por título\n6 - Retirar para ler" +
            "\n7 - Emprestar livro\n8 - Livro terminado\n9 - Livro devolvido\n10 - Sair\n\n" +
            "Escolha uma opção: ");

        var aux = VerifyInt();

        return aux;
    }

    private static void UserEditInterface(IMongoCollection<BsonDocument> collection, string filter)
    {
        bool condition = true;

        do
        {
            Console.Clear();
            int option = EditBookMenu();

            switch (option)
            {

                case 1:
                    Console.Clear();

                    UpdateBook(collection, filter, "Title");

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.Clear();

                    UpdateBook(collection, filter, "Author");

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();

                    UpdateBook(collection, filter, "Publisher");

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.Clear();

                    UpdateBook(collection, filter, "Publication Year");

                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 5:
                    condition = false;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida");
                    Console.WriteLine("Aperte qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
            }
        } while (condition);
    }

    private static int EditBookMenu()
    {
        Console.WriteLine(">>>Menu de opções<<<\n\n1 - Editar Titulo \n2 - Editar Autor\n" +
            "3 - Editar Editora\n4 - Editar Ano de Publicação \n5 - Sair\n\n" +
            "Escolha uma opção: ");

        var aux = VerifyInt();

        return aux;

    }
    #endregion

    #region [C]reate
    private static void RegisterBook(IMongoCollection<BsonDocument> collection)
    {
        Console.WriteLine("Titulo: ");
        string title = Console.ReadLine();

        Console.WriteLine("ISNB: ");
        string isnb = Console.ReadLine();

        Console.WriteLine("Editora: ");
        string publisher = Console.ReadLine();

        Console.WriteLine("Ano de publicação: ");
        string runYear = Console.ReadLine();

        Console.WriteLine("Nome do autor: ");
        string authorName = Console.ReadLine();

        Console.WriteLine("Data de Nascimento: ");
        string authorBirth = Console.ReadLine();

        var bookSon = new BsonDocument
        {
            {"Title", title},
            {"ISNB", isnb },
            {"Publisher", publisher},
            {"Publication Year", runYear},
            {"Author", new BsonDocument()
                {
                    {"Name", authorName},
                    {"Birth Date", authorBirth}
                }
            }
        };
        collection.InsertOne(bookSon);
    }

    private static void InsertBook(IMongoCollection<BsonDocument> collectionOut, IMongoCollection<BsonDocument> collectionIn, string findTitle)
    {
        var filter = Builders<BsonDocument>.Filter.Regex("Title", findTitle);
        var bsonBook = collectionOut.Find(filter).First();

        collectionIn.InsertOne(bsonBook);
    }
    #endregion

    #region [R]ead
    private static void ShowBookcase(IMongoCollection<BsonDocument> collection)
    {
        var documents = collection.Find(new BsonDocument()).ToList();
        documents.ForEach(item => Console.WriteLine(BsonSerializer.Deserialize<Book>(item).ToString() + "\n\n"));

        //foreach (var item in documents)
        //{
        //    var book = BsonSerializer.Deserialize<Book>(item);
        //    Console.WriteLine(book.ToString());
        //    Console.ReadLine();
        //    Console.Clear();
        //}
    }

    private static void ShowBookByTitle(IMongoCollection<BsonDocument> collection, string findTitle)
    {
        var filter = Builders<BsonDocument>.Filter.Regex("Title", findTitle);
        var bsonShelf = collection.Find(filter).FirstOrDefault();

        var deserializedBook = BsonSerializer.Deserialize<Book>(bsonShelf);

        Console.WriteLine();
        Console.WriteLine(deserializedBook.ToString());
        Console.WriteLine();
    }
    #endregion

    #region [U]pdate
    private static void UpdateBook(IMongoCollection<BsonDocument> collection, string findTitle, string fieldEdit)
    {
        Console.WriteLine($"Digite o(a) novo(a) {fieldEdit}: ");
        string newAttribute = Console.ReadLine();

        var filter = Builders<BsonDocument>.Filter.Regex("Title", findTitle);
        var update = Builders<BsonDocument>.Update.Set(fieldEdit, newAttribute);

        collection.UpdateOne(filter, update);
    }
    #endregion

    #region [D]elete
    private static void DeleteBook(IMongoCollection<BsonDocument> collection, string findTitle)
    {
        var filter = Builders<BsonDocument>.Filter.Regex("Title", findTitle);

        collection.FindOneAndDelete(filter);
    }
    #endregion

    #region Verificacao
    private static char VerifyChar()
    {
        char value;
        do
        {
            if (!char.TryParse(Console.ReadLine().ToLower(), out value))
                Console.WriteLine("Digite um caractere [S] ou [N]");
            else
                return value;

        } while (true);
    }

    private static int VerifyInt()
    {
        int value;
        do
        {
            if (!int.TryParse(Console.ReadLine(), out value))
                Console.WriteLine("Digite um número inteiro");
            else
                return value;

        } while (true);
    }
    #endregion
}