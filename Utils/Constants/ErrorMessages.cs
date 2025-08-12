namespace LibraryTask.Utils.Constants
{
    public static class ErrorMessages
    {
        public const string InvalidYear = "Invalid year.";
        public const string IsbnAlreadyExists = "ISBN already in use.";
        public const string BookNotFound = "Book not found.";
        public const string ConflictInId = "Id in body and URL do not match.";
        public const string DatabaseAddError = "Unable to add book.";
        public const string DatabaseUpdateError = "Unable to update book.";
        public const string DatabaseDeleteError = "Unable to delete book.";
    }
}
