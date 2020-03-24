namespace Photo_Album
{
    public static class Constants
    {
        public const string WELCOME = "Welcome to Photo Album!";
        public const string ASK_FOR_ID = "Please enter the ID of the album you want:";
        public const string ASK_IF_CONTINUE = "Would you like to continue? (Y/N)";
        public const string PHOTO_RECORD = "[{0}] {1}";
        public const string NO = "N";
        
        public const string PHOTOS_URL = "https://jsonplaceholder.typicode.com/photos";
        public const string QUERY_BY_ALBUM_ID_SUFFIX = "?albumId=";
        
        public const string ERROR_IS_NOT_NUMBER = "The value you entered is not a number.";
        public const string ERROR_FAILED_CONNECTION = "Failed to connect to Album Service. ResponseStatusCode:{0}, ReasonPhrase:{1}";
    }
}
