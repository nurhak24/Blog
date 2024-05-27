namespace Blog.Web.ResultMessages
{
    public static class Messages
    {

        public static class Article
        {

            public static string Add(string articleTittle)
            {

                return $"{articleTittle} başlıklı makale başarıyla eklenmiştir.";

            }
            public static string Update(string articleTittle)
            {

                return $"{articleTittle} başlıklı makale başarıyla değiştirilmiştir.";

            }
            public static string Delete(string articleTittle)
            {

                return $"{articleTittle} başlıklı makale başarıyla silinmiştir.";

            }
            public static string UndoDelete(string articleTittle)
            {

                return $"{articleTittle} başlıklı makale başarıyla silinmiştir.";

            }

        }

        public static class Category
        {

            public static string Add(string CategoryTittle)
            {

                return $"{CategoryTittle} başlıklı makale başarıyla eklenmiştir.";

            }
            public static string Update(string CategoryTittle)
            {

                return $"{CategoryTittle} başlıklı makale başarıyla değiştirilmiştir.";

            }
            public static string Delete(string CategoryTittle)
            {

                return $"{CategoryTittle} başlıklı makale başarıyla silinmiştir.";

            }
            public static string UndoDelete(string CategoryTittle)
            {

                return $"{CategoryTittle} başlıklı makale başarıyla silinmiştir.";

            }


        }

        public static class User
        {

            public static string Add(string userName)
            {

                return $"{userName} başlıklı makale başarıyla eklenmiştir.";

            }
            public static string Update(string userName)
            {

                return $"{userName} başlıklı makale başarıyla değiştirilmiştir.";

            }
            public static string Delete(string userName)
            {

                return $"{userName} başlıklı makale başarıyla silinmiştir.";

            }


        }


    }
}
