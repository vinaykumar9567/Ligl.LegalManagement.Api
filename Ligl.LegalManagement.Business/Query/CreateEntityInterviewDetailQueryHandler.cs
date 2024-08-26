using Ligl.LegalManagement.Model.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using Ligl.LegalManagement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;
using Ligl.LegalManagement.Model.Common;
using Ligl.LegalManagement.Model.Query.Constants;
using Ligl.LegalManagement.Model.Query.CustomModels;
using InterviewEntity = Ligl.LegalManagement.Repository.Domain.InterviewEntity;
using Ligl.Core.Sdk.Shared.Repository.Region.Domain;
using EntityType = Ligl.LegalManagement.Model.Common.EntityType;


namespace Ligl.LegalManagement.Business.Query
{
    /// <summary>
    /// Class for CreateEntityInterviewDetailQueryHandler
    /// </summary>
    /// <seealso cref="CreateEntityInterviewDetailQuery" />
    public class CreateEntityInterviewDetailQueryHandler(IRegionUnitOfWork regionUnitOfWork, ILookUpBusiness lookUpBusiness, ILogger<CreateEntityInterviewDetailQueryHandler> logger,
      IEntityBusiness entityBusiness) : IRequestHandler<CreateEntityInterviewDetailQuery, Unit>
    {
        private const string ClassName = nameof(CreateEntityInterviewDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<Unit> Handle(CreateEntityInterviewDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                Guid? CaseCustodian = entityBusiness.GetDomainValueById(id: EntityType.CaseCustodian.id).Uuid;
                logger.LogInformation(message: "Started execution of {methodName}", methodName);
                int entityTypeID = 1;
                var entityID = GetEntityID(request.InterviewEntityViewModel.EntityTypeUniqueID,
                       request.InterviewEntityViewModel.EntityUniqueID);
                int? caseLegalHoldID = (await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync()).Where(x => x.UUID == request.InterviewEntityViewModel.CaseLegalHoldUniqueID).FirstOrDefault()?.CaseLegalHoldID;

                var dbInterview = new InterviewEntityViewModel
                {
                    CaseLegalHoldID =(int) caseLegalHoldID,
                    EntityTypeID = entityTypeID,
                    EntityID =  entityID
                };

                dbInterview =InterviewMapper.InterviewEntityMapper(request.InterviewEntityViewModel, dbInterview, true);
                var interview = new InterviewEntity()
                {
                    UUID=dbInterview.UUID,
                    EntityID=dbInterview.EntityID,
                    EntityTypeID=dbInterview.EntityTypeID,
                    Interviewer=dbInterview.Interviewer,
                    InterviewPlace=dbInterview.InterviewPlace,
                    Notes=dbInterview.Notes,
                    Status=dbInterview.Status,
                    CaseLegalHoldID=caseLegalHoldID,
                    InterviewDate = dbInterview.InterviewDate,
                    ModifiedBy= "11d1c423-01d9-4723-b852-d8d8e5a4385f",
                    IsDeleted=false,
                   CreatedBy= "11d1c423-01d9-4723-b852-d8d8e5a4385f",
                   CreatedOn=DateTime.UtcNow,
                   ModifiedOn=DateTime.UtcNow

                };
                await regionUnitOfWork.InterviewEntityRepository.CreateAsync(interview);
                regionUnitOfWork.Save();
                if (request.InterviewEntityViewModel != null && request.InterviewEntityViewModel.Documents.Count > 0)
                {
                    Guid? Interview = lookUpBusiness.GetDomainValueById(id: EntityType.Interview.id).Uuid;
                    var documentModel = request.InterviewEntityViewModel.Documents.First();
                    var interviewEntityTypeID = (await regionUnitOfWork.EntityRepository.GetAsync())
                .FirstOrDefault(entity => entity.Uuid == Interview)?.EntityId;
                   
                    var entityState = EntityState.Added;
                    var dbDocumentStream = DocumentStreamMapper.DocumentEntityMapper(documentModel,
                        new DocumentStreamEntity(), request.InterviewEntityViewModel.EntityUniqueID, dbInterview.InterviewID, (int)interviewEntityTypeID, entityState == EntityState.Added, documentModel.FilePath);

                    var dbDocumentStreams = new DocumentStream
                    {
                         
                    Name = dbDocumentStream.Name,
                    Extension = dbDocumentStream.Extension,
                    Comments = dbDocumentStream.Comments,
                    FileSize = dbDocumentStream.FileSize,
                    FileData = dbDocumentStream.FileData,
                    ModifiedOn = DateTime.UtcNow,
                    ModifiedBy = ""
                };

                    await regionUnitOfWork.DocumentStreamRepository.UpdateAsync(dbDocumentStreams);
                }
                regionUnitOfWork.SaveAsync();
                regionUnitOfWork.CommitAsync();
                return Unit.Value;
            }
            catch (Exception e)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
                    methodName, e.Message, e.StackTrace);
                throw;
            }
            finally
            {
                logger.LogInformation(message: "Completed execution of {methodName}", methodName);
            }
        }



        private  int GetEntityID(Guid? entityTypeUniqueID, Guid? entityUniqueID)
        {
         
            const string methodName = nameof(GetEntityID);
            try
            {
                int? entityID = null;
                int? entityTypeID = null;
                if (entityTypeUniqueID == null || entityTypeUniqueID == Guid.Empty)
                {
                    logger.LogInformation(message:"{methodName} - not a valid entity typeID {ErrorType.Error} {ClassName}", methodName, ErrorType.Error, ClassName);
                    throw new CustomError(InterviewErrorCodes.NullEntityType,
                        BaseErrorProvider.GetErrorString<InterviewErrorCodes>(InterviewErrorCodes.NullEntityType), $"{ClassName} - {methodName}");
                }
                entityTypeUniqueID = new Guid("B18FB8F3-8ECA-417F-A007-E6EC25763E77");
                var entityType = ( regionUnitOfWork.EntityRepository.GetAsync()).Result.FirstOrDefault(entity => entity.Uuid == entityTypeUniqueID);
               

                if (entityType == null)
                {
                    logger.LogError($"{methodName} - not a valid entity typeID ");
                    throw new CustomError(InterviewErrorCodes.NullEntityType,
                        BaseErrorProvider.GetErrorString<InterviewErrorCodes>(InterviewErrorCodes.NullEntityType), $"{ClassName} - {methodName}");
                }
                entityTypeID = entityType.EntityId;
                entityUniqueID = new Guid("6931CD19-B089-4308-B4C6-D57764FDCF0D");
                Guid? CaseCustodian = entityBusiness.GetDomainValueById(id: EntityType.CaseCustodian.id).Uuid;
                Guid? CaseStakeHolder = entityBusiness.GetDomainValueById(id: EntityType.CaseStakeHolder.id).Uuid;
                if (entityTypeUniqueID != CaseCustodian)
                   entityID =( regionUnitOfWork.CaseCustodianRepository.GetAsync()).Result
                        .FirstOrDefault(casecustodian => casecustodian.Uuid == entityUniqueID)?.CaseCustodianUniqueId;
                else if (entityTypeUniqueID == CaseStakeHolder)
                   entityID = ( regionUnitOfWork.caseStakeHolderEntity.GetAsync()).Result
                      .FirstOrDefault(casestakeholder => casestakeholder.Uuid == entityUniqueID)?.CaseStakeHolderId;

                if (entityID != null)
                    return (int)entityID;
          

                logger.LogInformation( message:"{methodName} - No Records found from DB {ErrorType.Error} {ClassName} ", methodName, ErrorType.Error, ClassName);
                throw new CustomError(InterviewErrorCodes.EntityNotExists,
                    BaseErrorProvider.GetErrorString<InterviewErrorCodes>(InterviewErrorCodes.EntityNotExists), $"{ClassName} - {methodName}");


            }
            catch (Exception ex)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
                    methodName, ex.Message, ex.StackTrace);
                throw;
            }

        }

    }
}
