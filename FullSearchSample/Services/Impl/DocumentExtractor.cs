namespace FullSearchSample.Services.Impl
{
    public class DocumentExtractor : IDocumentExtractor
    {
        public static IEnumerable<string> DocumentsSet()
        {
            return ReadDocuments(AppContext.BaseDirectory + "data.txt");
        }

        private static IEnumerable<string> ReadDocuments(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
                while (!streamReader.EndOfStream)
                {
                    var doc = streamReader.ReadLine().Split("\t");
                    yield return doc[1];
                }
        }

    }


}
