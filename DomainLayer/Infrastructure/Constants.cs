namespace BookLibrary.Domain.Core.Infrastructure
{
    public static class Constants
    {
        public static class Validation
        {
            public static class Books
            {
                public static string IncorrectId() =>
                    "Incorrect book id!";

                public static string BookNotFound(long id) =>
                    $"Book with id {id} not found!";

                public static string BooksNotFound() =>
                    "No books in database!";

                public static string SameBookTitleExists() =>
                    "Same book title already exists!";

                public const string TitleMaxLength = "Sorry, but max length of title is 30!";
                public const string TextContentMaxLength = "Sorry, but max length of text content is 1000!";
            }

            public static class Orders
            {
                public static string IncorrectId() =>
                    "Incorrect order id!";

                public static string OrderNotFound(string id) =>
                    $"Order with id {id} not found!";

                public static string OrdersNotFound() =>
                    "No orders in database!";
            }

            public static class Users
            {
                public static string IncorrectId() =>
                    "Incorrect user id!";

                public static string UserNotFound(string id) =>
                    $"ApplicationUser with id {id} not found!";

                public static string UsersNotFound() =>
                    "No users in database!";

                public static string SameUserExists() =>
                    "Same user already exists!";

                public const string FirstNameMaxLength = "Sorry, but max length of first name is 30!";
                public const string LastNameMaxLength = "Sorry, but max length of last name is 30!";
                public const string EmailMaxLength = "Sorry, but max length of email is 30!";
                public const string EmailError = "Check whether your email is correct!";
            }

            public static class CommonErrors
            {
                public static string ServerError(string message) =>
                    $"Seems that something went wrong with server: {message}";

                public static string SQLError(string message) =>
                    $"Seems that something went wrong with database: {message}";

                public static string BookLibraryStorageError(string message) =>
                    $"Seems that something went wrong with content storage: {message}";

                public static string CosmosError(string message) =>
                    $"Seems that something went wrong with CosmosDB: {message}";

                public static string IncorrectDataProvided() =>
                    "Incorrect data provided!";
            }
        }
    }
}
