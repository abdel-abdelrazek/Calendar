using System.ComponentModel.DataAnnotations;


namespace Calendar.Models
{
    /// <summary>
    /// This helps for validation, by writing validation attributes 
    /// in a file rather than in the model 
    /// </summary>
    public class PartialClasses
    {

        [MetadataType(typeof(EventMetadata))]
        public partial class EventModels
        {
        }
    }
}