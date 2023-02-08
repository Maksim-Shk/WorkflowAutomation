namespace WorkflowAutomation.Application.Documents.Queries.GetAllowedDocumentList
{
    public class GetAllowedDocumentListLookupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? RemoveDate { get; set; }
        public string DocumentType { get; set; }
        public ShortUserInfo SenderInfo { get; set; }
        public ShortUserInfo RecieverInfo { get; set; }
        //TODO ещё чего

    }
}