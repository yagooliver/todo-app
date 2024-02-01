namespace TodoList.Domain.Entities
{
    public class Item : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public DateTime EndDate {get;set;}
        public bool ShowAlerts {get;set;}
        public string Comments {get;set;}
        public Guid? UserId { get; set;}

        public ItemStatus Status { get; set; }
        public virtual IList<ItemHistory> ItemHistories {get;set;}
    }
}
