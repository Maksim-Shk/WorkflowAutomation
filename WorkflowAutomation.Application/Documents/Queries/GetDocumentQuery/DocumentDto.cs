namespace WorkflowAutomation.Application.Documents.Queries.GetOneDocument
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? RemoveDate { get; set; }
        public string DocumentType { get; set; }
        public string SenderInfo { get; set; }
        public string RecieverInfo { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        public List<DocFile> Files { get; set; }

    }
}
