namespace SharedLibrary.Messages
{
    public class CourseNameChangedEvent
    {
        public string CourseId { get; set; }
        public string CourseNewName { get; set; }
    }
}
