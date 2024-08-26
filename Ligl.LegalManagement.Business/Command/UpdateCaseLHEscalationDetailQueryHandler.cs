using Ligl.LegalManagement.Model.Query.CustomModels;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ligl.LegalManagement.Model.Command;
using Ligl.Core.Sdk.Common.Helper;
using Ligl.LegalManagement.Model.Common;
using Ligl.LegalManagement.Model.Query.Constants;


namespace Ligl.LegalManagement.Business.Command
{
    /// <summary>
    /// Class for UpdateCaseLHEscalationDetailQueryHandler
    /// </summary>
    /// <seealso cref="UpdateCaseLHEscalationDetailQuery" />
    public class UpdateCaseLHEscalationDetailQueryHandler(IRegionUnitOfWork regionUnitOfWork, ILogger<UpdateCaseLHEscalationDetailQueryHandler> logger
      ) : IRequestHandler<UpdateCaseLHEscalationDetailQuery,Unit>
    {
        private const string ClassName = nameof(UpdateCaseLHEscalationDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<Unit> Handle(UpdateCaseLHEscalationDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);
                var remEscConfig = (await regionUnitOfWork.ReminderAndEscalationRepository.GetAsync()).FirstOrDefault(x => x.UUID == request.escalationConfig.EscalationReminderConfigID);
 
                    var remConfig = SerializationHelper.XmlToObject<ReminderConfig>(xml: remEscConfig?.ReminderConfig);
                    var escConfig = SerializationHelper.XmlToObject<EscalationConfig>(xml:remEscConfig?.ReminderConfig);
                    escConfig.Initialize.Value = request.escalationConfig.Initialize.Value;
                    escConfig.NotificationFrequency = request.escalationConfig.NotificationFrequency;
                    escConfig.NotificationCap = request.escalationConfig.NotificationCap;
                    escConfig.EmailTemplate = request.escalationConfig.EmailTemplate;
                    escConfig.EscalationEmail = request.escalationConfig.EscalationEmail;
                    escConfig.EscalationDeleted = request.escalationConfig.EscalationDeleted;

                    var reminderEscalationConfig = new EscalationReminderConfig
                    {
                        ReminderConfig = remConfig!,
                        EscalationConfig = escConfig,
                        EscalationReminderConfigID = request.escalationConfig.EscalationReminderConfigID
                    };


                var defaultConfigID = ReminderandEscalationTemplate.ReminderandEscalationTemplateID;
                //update default config of escalation and reminder in caselegalhold edit.
                if (reminderEscalationConfig.EscalationReminderConfigID == defaultConfigID)
                {
                    var defaultConfig = await regionUnitOfWork.ReminderAndEscalationRepository.GetReminderByIdAsync(defaultConfigID);
                    if (defaultConfig == null)
                    {
                        logger.LogError( "Error Processing {methodName}, {ErrorType.Error} ,{ClassName} " , methodName,ErrorType.Error, ClassName);
                        throw new CustomError(EntityNotificationErrorCodes.ReminderEscalationConfigNotFound,
                            BaseErrorProvider.GetErrorString<EntityNotificationErrorCodes>(EntityNotificationErrorCodes.ReminderEscalationConfigNotFound),
                            $"{ClassName} - {nameof(methodName)}");
                    }
                         defaultConfig.EscalationConfig = SerializationHelper.ObjectToXml(reminderEscalationConfig.EscalationConfig).ToString();
                    await regionUnitOfWork.ReminderAndEscalationRepository.UpdateAsync(defaultConfig);
                
                }
                //update escalation and reminder config of selected case legalhold(during case legal hold edit) or default(during case legalhold creation)
                var config = await regionUnitOfWork.ReminderAndEscalationRepository.GetReminderByIdAsync(reminderEscalationConfig.EscalationReminderConfigID);
                if (config == null)
                {
                 

                    logger.LogError( "Error Processing {methodName},{ErrorType.Error},{ClassName}", ErrorType.Error, ClassName, methodName);
                    throw new CustomError(EntityNotificationErrorCodes.ReminderEscalationConfigNotFound,
                            BaseErrorProvider.GetErrorString<EntityNotificationErrorCodes>(EntityNotificationErrorCodes.ReminderEscalationConfigNotFound),
                            $"{ClassName} - {nameof(methodName)}");
                }
                config.ReminderConfig = SerializationHelper.ObjectToXml(reminderEscalationConfig.ReminderConfig).ToString();
                config.EscalationConfig = SerializationHelper.ObjectToXml(reminderEscalationConfig.EscalationConfig).ToString();
                await regionUnitOfWork.ReminderAndEscalationRepository.UpdateAsync(config);


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



        /// <summary>
		///To Update Escalation And Reminder Config
		/// </summary>
		/// <param name="escalationReminderConfig"></param>
		/// <returns></returns>
		public async Task< Guid> UpdateEscalationAndReminderConfig(EscalationReminderConfig escalationReminderConfig)
        {
            const string methodName = nameof(UpdateEscalationAndReminderConfig);
            try
            {
               
                    var defaultConfigID = ReminderandEscalationTemplate.ReminderandEscalationTemplateID;
                    //update default config of escalation and reminder in caselegalhold edit.
                    if (escalationReminderConfig.EscalationReminderConfigID == defaultConfigID)
                    {
                        var defaultConfig = (await regionUnitOfWork.ReminderAndEscalationRepository.GetAsync()).FirstOrDefault(x => x.UUID == defaultConfigID);
                        if (defaultConfig == null)
                        {
                            logger.LogError("Error Processing {methodName} {ErrorType.Error} {ClassName}", methodName,ErrorType.Error, ClassName);
                            throw new CustomError(EntityNotificationErrorCodes.ReminderEscalationConfigNotFound,
                                BaseErrorProvider.GetErrorString<EntityNotificationErrorCodes>(EntityNotificationErrorCodes.ReminderEscalationConfigNotFound),
                                $"{ClassName} - {nameof(methodName)}");
                        }

                    //defaultConfig.ReminderConfig = SerializationHelper.XMLSerialize(escalationReminderConfig.ReminderConfig);
                    //defaultConfig.EscalationConfig = SerializationHelper.XMLSerialize(escalationReminderConfig.EscalationConfig);
                    await regionUnitOfWork.ReminderAndEscalationRepository.UpdateAsync(defaultConfig);
                    regionUnitOfWork.Save();
                }
                    //update escalation and reminder config of selected case legalhold(during case legal hold edit) or default(during case legalhold creation)
                    var config = (await regionUnitOfWork.ReminderAndEscalationRepository.GetAsync()).FirstOrDefault(x => x.UUID == escalationReminderConfig.EscalationReminderConfigID);
                    if (config == null)
                    {
                    logger.LogError("Error Processing {methodName}  ErrorType:{ErrorType.Error}  className : {ClassName}", methodName, ErrorType.Error, ClassName);
                    throw new CustomError(EntityNotificationErrorCodes.ReminderEscalationConfigNotFound,
                                BaseErrorProvider.GetErrorString<EntityNotificationErrorCodes>(EntityNotificationErrorCodes.ReminderEscalationConfigNotFound),
                                $"{ClassName} - {nameof(methodName)}");
                    }
                //config.ReminderConfig = SerializationHelper.XMLSerialize(escalationReminderConfig.ReminderConfig);
                //config.EscalationConfig = SerializationHelper.XMLSerialize(escalationReminderConfig.EscalationConfig);
                await regionUnitOfWork.ReminderAndEscalationRepository.UpdateAsync(config);
                regionUnitOfWork.Save();
                    return config.UUID;
                
            }
            catch (Exception ex)
            {
                logger.LogError("Error Processing {methodName}  ErrorType:{ErrorType.Error}  className : {ClassName}", methodName, ErrorType.Error, ClassName);
                throw new CustomError(EntityNotificationErrorCodes.FailureInUpdatingConfig,
                    BaseErrorProvider.GetErrorString<EntityNotificationErrorCodes>(EntityNotificationErrorCodes.FailureInUpdatingConfig),
                    $"{ClassName} - {nameof(methodName)}-{ex.StackTrace} - {ex.Message}");
            }
            finally
            {
                logger.LogInformation("Completed Processing {methodName} {ErrorType.Information} {ClassName}",methodName, ErrorType.Information, ClassName);
            }
        }


    }


}
