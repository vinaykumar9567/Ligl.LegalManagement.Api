using Ligl.LegalManagement.Model.Query;
namespace Ligl.LegalManagement.Business.Query
{

    /// <summary>
    /// DocumentStreamMapper class
    /// </summary>
    public class DocumentStreamMapper
    {
        /// <summary>
        /// Method to map DocumentStreamModel to DocumentStreamEntity
        /// </summary>
        /// <param name="documentStreamSource"></param>
        /// <param name="parentUniqueID"></param>
        /// <param name="documentStreamDestination"></param>
        /// <param name="EntityID"></param>
        /// <param name="EntityTypeID"></param>
        /// <param name="isAddMode"></param>
        /// <returns></returns>
        public static DocumentStreamEntity DocumentEntityMapper(DocumentStreamModel documentStreamSource,
            DocumentStreamEntity documentStreamDestination, Guid? parentUniqueID, int EntityID, int EntityTypeID,
            bool isAddMode = false, string filePath = null)
        {
            var currentDateTime = DateTime.UtcNow;
            documentStreamDestination.Name = documentStreamSource.Name;
            documentStreamDestination.Extension = documentStreamSource.Extension;
            documentStreamDestination.Comments = documentStreamSource.Comments;
            documentStreamDestination.FileSize = documentStreamSource.FileSize;
            documentStreamDestination.FileData = documentStreamSource.FileData;
            documentStreamDestination.ModifiedOn = currentDateTime;
            documentStreamDestination.ModifiedBy = "";
            if (!isAddMode)
                return documentStreamDestination;

            documentStreamDestination.UUID = Guid.NewGuid();
            documentStreamDestination.EntityID = EntityID;
            documentStreamDestination.FilePath = filePath ?? parentUniqueID.ToString();
            documentStreamDestination.EntityTypeID = EntityTypeID;
            documentStreamDestination.CreatedOn = currentDateTime;
            documentStreamDestination.CreatedBy = "";
            documentStreamDestination.IsDeleted = false;
            documentStreamDestination.LHNPreservingTypeID = documentStreamSource.LHNPreservingTypeID ;

            return documentStreamDestination;
        }
    }
}
