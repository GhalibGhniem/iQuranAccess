// Author:					Ghalib Ghniem ghalib@ItInfoPlus.com
// Created:				    2015-03-22
// Last Modified:			2015-03-31
// 
 
using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Reflection;
using System.ComponentModel;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Business;
using mojoPortal.Business.Commerce;
using mojoPortal.Business.WebHelpers;
using iQuran.Business;
using mojoPortal.Net;
using log4net;
using Resources;

namespace iQuran.Web.Helper
{

	public static class iQuranHelper
    {


		private static readonly ILog log = LogManager.GetLogger(typeof(iQuranHelper));

        // using mojoPortal.Web.Framework; ConfigHelper.GetStringProperty("iQuranAdvancedRoles", string.Empty);

        #region PATH FINDER

        //D:\inetpub\RFPUHS\RFP\Web\Data\Sites\1\UHSRFP\ProposalFiles\RFP32\Proposal51\Addendum3\Attachments
        //D:\inetpub\RFPUHS\RFP\Web\Data\Sites\1\UHSRFP\ProposalFiles\RFP32\Proposal50\Addendum2\DeliverableSamples


		//public static string GetProposalVirtualFolders(int siteId, int rfpID, int proposalID, int addendumSerial)
		//{
		//	string requestedFolder = string.Empty;
		//	requestedFolder = SiteUtils.GetNavigationSiteRoot() + "/Data/Sites/" + siteId + "/UHSRFP/ProposalFiles/RFP" + rfpID + "/Proposal" + proposalID + "/Addendum" + addendumSerial + "/";
		//	return requestedFolder;
		//}

        public static string GetAppPath()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }

        public static string SitesFolderPath()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath + "Data\\Sites\\";
        }

        public static string SiteFolderPath(int siteid)
        {
            return HttpContext.Current.Request.PhysicalApplicationPath + "Data\\Sites\\" + siteid;
        }

		//public static string ProposalFolderPath(int siteid, int rfpID, int proposalID, int addendumSerial)
		//{
		//	return HttpContext.Current.Request.PhysicalApplicationPath + "Data\\Sites\\" + siteid + "\\UHSRFP\\ProposalFiles\\RFP" + rfpID + "\\Proposal" + proposalID + "\\Addendum" + addendumSerial;
		//}

        #endregion

		//#region RFP DIRECTORY

		////CREATE
		//public static string CreateRfpFolders(int siteId, int rfpID, int addendumSerial)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\RfpFiles\\" + rfpID + "\\RfpStaff" + "\\Addendum" + addendumSerial;

		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	return requestedFolder;
		//}
        
		////DELETE
		//public static void DeleteRfpFolderFromDisk(int siteId, int rfpID)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\RfpFiles\\" + rfpID;


		//	if (Directory.Exists(requestedFolder))
		//		Directory.Delete(requestedFolder, true);
		//}

		//public static void DeleteRfpAddendumFolderFromDisk(int siteId, int rfpID, int addendumSerial)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\RfpFiles\\" + rfpID + "\\RfpStaff" + "\\Addendum" + addendumSerial;

		//	if (Directory.Exists(requestedFolder))
		//		Directory.Delete(requestedFolder, true);
		//}

		//public static void DeleteAddendumAttachmentFromDisk(int siteId, int rfpID, int addendumSerial, string fileName)
		//{
		//	string requestedFile = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFile = path + "\\UHSRFP\\RfpFiles\\" + rfpID + "\\RfpStaff" + "\\Addendum" + addendumSerial + "\\" + fileName;

		//	if (File.Exists(requestedFile))
		//		File.Delete(requestedFile);
		//}
        
		////GET
		//public static string GetRfpFolders(int siteId, int rfpID, int addendumSerial)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\RfpFiles\\" + rfpID + "\\RfpStaff" + "\\Addendum" + addendumSerial + "\\";

		//	return requestedFolder;
		//}

		//#endregion

		//#region LIBRARY DIRECTORY

		////CREATE
		//public static string CreateDeliverableFolder(int siteId, int servSubCatID, int deliverableID)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\DeliverableSamples\\" + servSubCatID + "_" + deliverableID;


		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	return requestedFolder;
		//}

		//public static string CreateTemplateFolders(int siteId, string tmplateName)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\Templates\\" + tmplateName;

		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	return requestedFolder;
		//}

		//public static string CreateUhsAttachmentFolders(int siteId, int uhsAttachmentCatID)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\UhsAttachments\\" + uhsAttachmentCatID;

		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	return requestedFolder;
		//}

		//public static string CreateServCatFolder(int siteId)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\ServCat";

		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	return requestedFolder;
		//}

		////DELETE
		//public static void DeleteDeliverableFolderFromDisk(int siteId, int servSubCatID, int deliverableID)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\DeliverableSamples\\" + servSubCatID + "_" + deliverableID ;

		//	if (Directory.Exists(requestedFolder))
		//		Directory.Delete(requestedFolder, true);
		//}

		//public static void DeleteDeliverableSampleFromDisk(int siteId, int servSubCatID, int deliverableID, string fileName)
		//{
		//	string requestedFile = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFile = path + "\\UHSRFP\\DeliverableSamples\\" + servSubCatID + "_" + deliverableID + "\\" + fileName;


		//	if (File.Exists(requestedFile))
		//		File.Delete(requestedFile);


		//}

		//public static void DeleteTemplateFromDisk(int siteId, string tmplateName)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\Templates\\" + tmplateName ;


		//	if (Directory.Exists(requestedFolder))
		//		Directory.Delete(requestedFolder, true);


		//}

		//public static void DeleteUhsAttachmentFolderFromDisk(int siteId, int uhsAttachmentCatID)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\UhsAttachments\\" + uhsAttachmentCatID;

		//	if (Directory.Exists(requestedFolder))
		//		Directory.Delete(requestedFolder, true);
		//}

		//public static void DeleteUhsAttachmentFromDisk(int siteId, int uhsAttachmentCatID, string fileName)
		//{
		//	string requestedFile = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFile = path + "\\UHSRFP\\UhsAttachments\\" + uhsAttachmentCatID + "\\" + fileName;

		//	if (File.Exists(requestedFile))
		//		File.Delete(requestedFile);
		//}

		//public static void DeleteServCatFromDisk(int siteId, string imageName)
		//{
		//	string requestedFile = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFile = path + "\\UHSRFP\\ServCat\\" + imageName;

		//	if (File.Exists(requestedFile))
		//		File.Delete(requestedFile);


		//}

		////GET
		//public static string GetDeliverableFolder(int siteId, int servSubCatID, int deliverableID)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\DeliverableSamples\\" + servSubCatID + "_" + deliverableID + "\\";
		//	return requestedFolder;
		//}

		//public static string GetTemplateFolder(int siteId, string tmplateName)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\Templates\\" + tmplateName + "\\";
		//	return requestedFolder;
		//}

		//public static string GetUhsAttachmentFolder(int siteId, int uhsAttachmentCatID)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\UhsAttachments\\" + uhsAttachmentCatID + "\\";
		//	return requestedFolder;
		//}
        
		//public static string GetServCatFolder(int siteId)
		//{
		//	string requestedFolder = string.Empty;
		//	string path = SiteFolderPath(siteId);

		//	requestedFolder = path + "\\UHSRFP\\ServCat\\" ;
		//	return requestedFolder;
		//}

		//#endregion

		//#region PROPOSAL DIRECTORY
		////CREATE
		//public static string CreateProposalFolders(int siteId, int rfpID, int proposalID, int addendumSerial)
		//{
		//	string requestedFolder = string.Empty;
		//	string proposalFolderPath = ProposalFolderPath(siteId, rfpID, proposalID, addendumSerial);

		//	requestedFolder = proposalFolderPath;

		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	requestedFolder = proposalFolderPath + "\\Attachments";

		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	requestedFolder = proposalFolderPath + "\\DeliverableSamples";

		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	return requestedFolder;
		//}

		//public static string CreateProposalAttachmentOrSampleFolders(int siteId, int rfpID, int proposalID, int addendumSerial, int ProposalAttachmentCatID, int proposalDeliverableID, string WhichFolder)
		//{
		//	string requestedFolder = string.Empty;
		//	string proposalFolderPath = ProposalFolderPath(siteId, rfpID, proposalID, addendumSerial);

		//	switch (WhichFolder)
		//	{
		//		case "Attachment":
		//				requestedFolder = proposalFolderPath + "\\Attachments\\" + ProposalAttachmentCatID + "\\";
		//				break;

		//		case "Sample":
		//				requestedFolder = proposalFolderPath + "\\DeliverableSamples\\" + proposalDeliverableID + "\\";
		//				break;
		//	}

		//	if (!Directory.Exists(requestedFolder))
		//		Directory.CreateDirectory(requestedFolder);

		//	return requestedFolder;
		//}

        
		////GET
		//public static string GetProposalFolders(int siteId, int rfpID, int proposalID, int addendumSerial, int ProposalAttachmentCatID, int proposalDeliverableID, string WhichFolder)
		//{
		//	string requestedFolder = string.Empty;
		//	string proposalFolderPath = ProposalFolderPath(siteId, rfpID, proposalID, addendumSerial);

		//	switch (WhichFolder)
		//	{
		//		case "Proposal":
		//		default:
		//			requestedFolder = proposalFolderPath + "\\";
		//			break;

		//		case "Attachment":
		//			if (ProposalAttachmentCatID > 0)
		//				requestedFolder = proposalFolderPath + "\\Attachments\\" + ProposalAttachmentCatID + "\\";
		//			else
		//				requestedFolder = proposalFolderPath + "\\Attachments\\";
		//			break;

		//		case "Sample":
		//			if (proposalDeliverableID > 0)
		//				requestedFolder = proposalFolderPath + "\\DeliverableSamples\\" + proposalDeliverableID + "\\";
		//			else
		//				requestedFolder = proposalFolderPath + "\\DeliverableSamples\\";
		//			break;
		//	}

		//	return requestedFolder;
		//}

      

		////DELETE FILE
		//public static void DeleteProposalFile(int siteId, int rfpID, int proposalID, int addendumSerial, int ProposalAttachmentCatID, int proposalDeliverableID, string fileName, string WhichFolder)
		//{
		//	string requestedFile = string.Empty;
		//	string proposalFolderPath = ProposalFolderPath(siteId, rfpID, proposalID, addendumSerial);

		//	switch (WhichFolder)
		//	{
		//		case "Proposal":
		//		default:
		//			requestedFile = proposalFolderPath + "\\" + fileName;
		//			break;

		//		case "Attachment":
		//			if (ProposalAttachmentCatID > 0)
		//				requestedFile = proposalFolderPath + "\\Attachments\\" + ProposalAttachmentCatID + "\\" + fileName;
		//			else
		//				requestedFile = proposalFolderPath + "\\Attachments\\" + fileName;
		//			break;

		//		case "Sample":
		//			if (proposalDeliverableID > 0)
		//				requestedFile = proposalFolderPath + "\\DeliverableSamples\\" + proposalDeliverableID + "\\" + fileName;
		//			else
		//				requestedFile = proposalFolderPath + "\\DeliverableSamples\\" + fileName;
		//			break;
		//	}

		//	if (File.Exists(requestedFile))
		//		File.Delete(requestedFile);
		//}

		////DELETE FOLDER
		//public static void DeleteProposalFolders(int siteId, int rfpID, int proposalID, int addendumSerial, int ProposalAttachmentCatID, int proposalDeliverableID, string WhichFolder)
		//{
		//	string requestedFolder = string.Empty;
		//	string proposalFolderPath = ProposalFolderPath(siteId, rfpID, proposalID, addendumSerial);

		//	switch (WhichFolder)
		//	{
		//		case "Proposal":
		//		default:
		//			requestedFolder = proposalFolderPath + "\\";
		//			break;

		//		case "Attachment":
		//			if (ProposalAttachmentCatID > 0)
		//				requestedFolder = proposalFolderPath + "\\Attachments\\" + ProposalAttachmentCatID + "\\";
		//			else
		//				requestedFolder = proposalFolderPath + "\\Attachments\\";
		//			break;

		//		case "Sample":
		//			if (proposalDeliverableID > 0)
		//				requestedFolder = proposalFolderPath + "\\DeliverableSamples\\" + proposalDeliverableID + "\\";
		//			else
		//				requestedFolder = proposalFolderPath + "\\DeliverableSamples\\";
		//			break;
		//	}


		//	if (Directory.Exists(requestedFolder))
		//		Directory.Delete(requestedFolder, true);

		//}

		//#endregion

		//#region FILEWORKS

		//public static void CopyFilesArrayFromSourceToTargetTemplateFiles(string[] filePaths, string target)
		//{
		//	bool overwrite = true;

		//	//string mFolder = SiteFolderPath(siteId) + "\\UHSRFP\\ChartTmp\\Proposal\\" + userid + "\\" + WhichFolder + "\\";
		//	string trgtFile = string.Empty;
		//	//string[] filePaths = Directory.GetFiles(src);
		//	foreach (string filePath in filePaths)
		//	{
		//		trgtFile = target + filePath.Substring(filePath.LastIndexOf("\\") + 1);
		//		File.Copy(filePath, trgtFile, overwrite);
		//	}



		//}

		//public static void CopyFilesFromSourceToTargetTemplateFiles(string src, string oldProposal, string newProposal, string oldAddendum, string newAddendum)
		//{
		//	bool overwrite = true;

		//	string trgtFolder = string.Empty; //SiteFolderPath(siteId) + "\\UHSRFP\\ChartTmp\\Proposal\\" + userid + "\\" + WhichFolder + "\\";
		//	string trgtFile = string.Empty;
		//	string[] filePaths = Directory.GetFiles(src,"*.*",SearchOption.AllDirectories);
		//	foreach (string filePath in filePaths)
		//	{
		//		//		filePath	"D:\\inetpub\\RFPUHS\\RFP\\Web\\Data\\Sites\\1\\UHSRFP\\ProposalFiles\\RFP33\\Proposal55\\Addendum2\\org3.jpg"	string
		//		//		target	"D:\\inetpub\\RFPUHS\\RFP\\Web\\Data\\Sites\\1\\UHSRFP\\ProposalFiles\\RFP33\\Proposal57\\Addendum3\\"	string

		//		trgtFile = filePath.Replace(oldProposal, newProposal).Replace(oldAddendum, newAddendum);
		//		trgtFolder = trgtFile.Replace(trgtFile.Substring(filePath.LastIndexOf("\\") + 1),"");
		//		//  trgtFile = target + filePath.Substring(filePath.LastIndexOf("\\") + 1);
		//		if (!Directory.Exists(trgtFolder))
		//			Directory.CreateDirectory(trgtFolder);

		//		File.Copy(filePath, trgtFile, overwrite);
		//	}
            

        
		//}

		//private static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		//{
		//	string[] searchPatterns = searchPattern.Split('|');
		//	List<string> files = new List<string>();
		//	foreach (string sp in searchPatterns)
		//		files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
		//	files.Sort();
		//	return files.ToArray();
		//}

		//public static void DeleteAllTemplateImages(string src)
		//{
		//	string[] filePaths = GetFiles(src, "*.jpg|*.gif|*.html|*.mht", SearchOption.TopDirectoryOnly);
		//	foreach (string filePath in filePaths)
		//	{
		//		if (File.Exists(filePath))
		//		File.Delete(filePath);
		//	}



		//}


		//public static void CopyFileFromSourceToTargetFolder(string src, string target)
		//{
		//	bool overwrite = true;

		//	if (File.Exists(src))
		//		File.Copy(src, target, overwrite);

		//}

		//public static bool CheckFileExists(string target)
		//{
		//	if (File.Exists(target))
		//		return true;
		//	else
		//		return false;

		//}

		//public static void CreateFile(string target)
		//{
		//	if (!File.Exists(target))
		//		File.Create(target);
		//}

		//#endregion

		//#region EMAIL


		//public static void SendEmailNotification(string status, string mUserFullName, string mUserID, string mPageLink,
		//   string RFPInitiateDatetime, string RfpProjectName, string RfpClientRefference,
		//   string RfpRefference, string addendumSerial,
		//   SiteSettings siteSettings)
		//{
		//	// status : SubmitRfp, ReviewRfp, cancelRfp, addendumRfp // SubmitProposal, ApproveProposal, PublishProposal , SendProposal , DeleteProposal

		//	//string loggedUser = ((mojoPortal.Business.SiteUser)(System.Web.HttpContext.Current.Items["CurrentUser"])).Name;
		//	CultureInfo defaultCulture = SiteUtils.GetDefaultUICulture();
		//	SmtpSettings smtpSettings = SiteUtils.GetSmtpSettings();
		//	EmailMessageTask messageTask = new EmailMessageTask(smtpSettings);

		//	string subjectTemplate = string.Empty;
		//	string textBodyTemplate = string.Empty;
		//	string adminEmail = ConfigHelper.GetStringProperty("NotifyAdminEmail", string.Empty);
		//	string adminRfpEmail = ConfigHelper.GetStringProperty("NotifyRfpAdminEmail", string.Empty);

		//	switch (status)
		//	{
		//		case "SubmitRfp":
		//		default:
		//			subjectTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsRfpSubmitMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsRfpSubmitMessage.config");
		//			break;

		//		case "ReviewRfp":
		//			subjectTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsRfpReviewMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsRfpReviewMessage.config");
		//			break;

		//		case "AddendumRfp":
		//			subjectTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsRfpAddendumMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsRfpAddendumMessage.config");
		//			break;

		//		case "CancelRfp":
		//			subjectTemplate  = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsRfpCancelMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsRfpCancelMessage.config");
		//			break;
                
		//		case "SubmitProposal":
		//			subjectTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsProposalSubmitMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsProposalSubmitMessage.config");
		//			break;

		//		case "ApproveProposal":
		//			subjectTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsProposalApproveMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsProposalApproveMessage.config");
		//			break;

		//		case "PublishProposal":
		//			subjectTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsProposalPublishMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsProposalPublishMessage.config");
		//			break;

		//		case "SendProposal":
		//			subjectTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsProposalSendMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsProposalSendMessage.config");
		//			break;

		//		case "DeleteProposal":
		//			subjectTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//				   "UhsProposalDeleteMessageSubject.config");
		//			textBodyTemplate = ResourceHelper.GetMessageTemplate(defaultCulture,
		//					   "UhsProposalDeleteMessage.config");
		//			break;

		//	}


		//	messageTask.EmailCc = adminRfpEmail;
		//	messageTask.EmailFrom = ConfigHelper.GetStringProperty("NotifyFromEmail", string.Empty);
		//	messageTask.EmailTo = adminEmail;
		//	messageTask.EmailCc = adminRfpEmail;

		//	messageTask.SiteGuid = siteSettings.SiteGuid;


		//	string SeperatorLine = ConfigHelper.GetStringProperty("SeperatorLine", string.Empty);
		//	string ContactUsUrl = ConfigHelper.GetStringProperty("ContactUsUrl", string.Empty);
		//	string PrivacyPolicyUrl = ConfigHelper.GetStringProperty("PrivacyPolicyUrl", string.Empty);
		//	string ServiceAgreementUrl = ConfigHelper.GetStringProperty("ServiceAgreementUrl", string.Empty);
		//	string UhsCompanyName = ConfigHelper.GetStringProperty("UhsCompanyName", string.Empty);
		//	string UhsPOBoxAddress = ConfigHelper.GetStringProperty("UhsAddress", string.Empty);
		//	string UhsTel = ConfigHelper.GetStringProperty("UhsTel", string.Empty);
		//	string UhsFax = ConfigHelper.GetStringProperty("UhsFax", string.Empty);
		//	string UhsMob = ConfigHelper.GetStringProperty("UhsMob", string.Empty);
		//	string CurrentYear = DateTime.Now.Year.ToString();
		//	string CurrentDatetime = String.Format(DateTime.Now.ToString(), "mm dd yyyy");


		//	messageTask.Subject = string.Format(
		//		  defaultCulture,
		//		  subjectTemplate
		//		  );

		//	messageTask.TextBody = string.Format(
		//			defaultCulture,
		//			textBodyTemplate,
		//			mUserID,
		//			mUserFullName,
		//			mPageLink,
		//			RFPInitiateDatetime,
		//			RfpProjectName,
		//			RfpClientRefference,
		//			RfpRefference,
		//		SeperatorLine, CurrentDatetime, ContactUsUrl, PrivacyPolicyUrl, ServiceAgreementUrl,
		//		CurrentYear, UhsCompanyName,
		//		UhsTel, UhsFax, UhsMob, UhsPOBoxAddress,
		//		   addendumSerial);


		//	messageTask.QueueTask();
		//	WebTaskManager.StartOrResumeTasks();

		//}


		//#endregion






































        #region "old"

        //public static bool LogEvent(
        //    bool Log, 
        //    bool RiskLog,
        //    string userActionStringResorcesName,
        //    string ActionDetailsStringResorcesName,
        //    string moreDetails,
        //    bool isAdmin,
        //    int autoMEPID,
        //    int siteID,
        //    int actionBy,
        //    string autoMEPName,
        //    string siteName,
        //    string actionByName

        //    )
        //{

        //    //User Actions Details Description:
        //    //User {0} - {1} <br /> From Site - {2} - {3} <br /> Made the next Action : Send BOQ Report  / {4}
        //    //User {actionBy:UserID} - {UserFullName} - From Site - {SiteID} - {SiteName} Made the next Action : Send BOQ Report  / {The Thing that Action made on}
        //    //User 3 - Ghalib Ghniem From Site - 1 - www.AutoMEPs.COM Made the next Action xxxx - Item Details

        //    bool res = false;
        //    CultureInfo defaultCulture = SiteUtils.GetDefaultUICulture();
        //    string Details = String.Format(defaultCulture, ActionDetailsStringResorcesName.Replace("_"," "),
        //        actionBy,
        //        actionByName,
        //        siteID,
        //        siteName,
        //        moreDetails
        //        );

        //    if (Log)
        //    {
        //        AutoMEPLog autoMEPLog = new AutoMEPLog();
        //        autoMEPLog.IsAdmin = isAdmin;
        //        autoMEPLog.AutoMEPID = autoMEPID;
        //        autoMEPLog.SiteID = siteID;
        //        autoMEPLog.ActionBy = actionBy;
        //        autoMEPLog.AutoMEPName = autoMEPName;
        //        autoMEPLog.SiteName = siteName;
        //        autoMEPLog.ActionByName = actionByName;
        //        autoMEPLog.UserAction = userActionStringResorcesName;
        //        autoMEPLog.Details = Details;

        //        res = autoMEPLog.Save();
        //    }

        //    if (RiskLog)
        //    {
        //        AutoMEPRiskLog autoMEPRiskLog = new AutoMEPRiskLog();
        //        autoMEPRiskLog.IsAdmin = isAdmin;
        //        autoMEPRiskLog.AutoMEPID = autoMEPID;
        //        autoMEPRiskLog.SiteID = siteID;
        //        autoMEPRiskLog.ActionBy = actionBy;
        //        autoMEPRiskLog.AutoMEPName = autoMEPName;
        //        autoMEPRiskLog.SiteName = siteName;
        //        autoMEPRiskLog.ActionByName = actionByName;
        //        autoMEPRiskLog.UserAction = userActionStringResorcesName;
        //        autoMEPRiskLog.Details = Details;

        //        res = autoMEPRiskLog.Save();
        //    }

        //    return res;

        //}
        ///// <summary>
        /////To Create Folders for each new AutoMEP System
        ///// </summary>
        //public static void CreateAutoMepFolders(int siteId)
        //{
        //    string requestedFolder = string.Empty;

        //    string path = HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\Sites\\" + siteId;

        //    requestedFolder = path + "\\AutoMEPData\\";

        //    if (!Directory.Exists(requestedFolder + "\\drawings"))
        //            Directory.CreateDirectory(requestedFolder + "\\drawings");


        //    if (!Directory.Exists(requestedFolder + "\\images\\thumb"))
        //            Directory.CreateDirectory(requestedFolder + "\\images\\thumb");


        //}

        //public static void DeleteUserFolders(int siteId, int userID)
        //{
        //    string path = HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\Sites\\" + siteId + "\\amsuserfiles\\" + userID;

        //    if (!Directory.Exists(path ))
        //        Directory.Delete(path);

          
           
        //}
        ///// <summary>
        ///// To retrive current user temp project folder to work in Or the current AutoMEP System Data Folder for a Project
        ///// </summary>
        ////public static string GetProjectFolder(string folderType, int siteId, int Idd, int projId, string moduleName)
        ////{
        ////    string requestedFolder = string.Empty;
        ////    string tmpStr = string.Empty;


        ////    if (folderType == "tmp")
        ////        requestedFolder = SiteUtils.GetNavigationSiteRoot() + "/Data/Sites/" + siteId + "/userfiles/" + Idd + "/" + projId;
        ////    else
        ////        tmpStr = SiteUtils.GetNavigationSiteRoot() + "/Data/Sites/" + siteId + "/AutoMEPData/" + Idd + "/" + projId;


        ////    switch (moduleName)
        ////    {
        ////        case "":
        ////        default:
        ////            break;

        ////        case "root":
        ////            requestedFolder = tmpStr;
        ////            break;

        ////        case "room":
        ////            requestedFolder = tmpStr + "/RoomsImages/";
        ////            break;

        ////        case "item":
        ////            requestedFolder = tmpStr + "/ItemsImages/";
        ////            break;

        ////        case "report":
        ////            requestedFolder = tmpStr + "/Reports/";
        ////            break;

        ////        case "cad":
        ////            requestedFolder = tmpStr + "/Drawings/";
        ////            break;

        ////    }

        ////}

        //public static Room GetRoomSession(int roomid, bool GetFull)
        //{
        //    if (HttpContext.Current == null) return null;


        //    if (HttpContext.Current.Items["Room"] != null)
        //    {
        //        return (Room)HttpContext.Current.Items["Room"];
        //    }

        //    Room room;

        //    if (roomid > 0)
        //        room = new Room(roomid, GetFull); 
        //    else
        //        room = new Room(); 

        //    if (room.RoomID > -1)
        //    {
        //        HttpContext.Current.Items.Add("Room", room);

        //        return room;
        //    }

        //    return null;
        //}

        //public static ItemLibrary GetItemSession(bool GetFull, bool UsingCode, bool UsingAbbrev, int libraryId, int itemid)
        //{
        //    if (HttpContext.Current == null) return null;


        //    if (HttpContext.Current.Items["ItemLibrary"] != null)
        //    {
        //        return (ItemLibrary)HttpContext.Current.Items["ItemLibrary"];
        //    }

        //    ItemLibrary itemlibrary;

        //    if (itemid > 0)
        //        itemlibrary = new ItemLibrary(GetFull, itemid);
        //    else
        //        itemlibrary = new ItemLibrary();

        //    if (itemlibrary.ItemID > -1)
        //    {
        //        HttpContext.Current.Items.Add("ItemLibrary", itemlibrary);

        //        return itemlibrary;
        //    }

        //    return null;
        //}

        ////public static Library GetLibrarySession(int libraryId, bool GetFull)
        ////{
        ////    if (HttpContext.Current == null) return null;


        ////    if (HttpContext.Current.Items["Library"] != null)
        ////    {
        ////        return (Library)HttpContext.Current.Items["Library"];
        ////    }

        ////    Library library;

        ////    if (libraryId > 0)
        ////        library = new Library(libraryId, GetFull);
        ////    else
        ////        library = new Library();

        ////    if (library.LibraryID > -1)
        ////    {
        ////        HttpContext.Current.Items.Add("Library", library);

        ////        return library;
        ////    }

        ////    return null;
        ////}

        //public enum amsFolder
        //{
        //    [DescriptionAttribute("ItemsImages")]
        //    itemImage,
        //    [DescriptionAttribute("ItemsDrawings")]
        //    itemDrawing,
        //    [DescriptionAttribute("RoomsImages")]
        //    room,
        //    vanilla
        //}
        /// <summary>
        /// To retrive current user temp folder to work in Or the current AutoMEP System Data Folder
        /// </summary>
        /// <param name="folderType">Either User Temp "tmp" Either AutoMEP System Data "ams" Folder</param> 
        /// <param name="siteId"></param>
        /// <param name="Idd">When AutoMEP System Data Folder "ams" It Will be "AutoMEPID"; When User Temp "tmp" It will be "UserId" </param> 
        /// <returns>requestedFolder</returns> 
        //public static string GetFolder(string folderType, int siteId, int Idd)
        //{
        //    string requestedFolder = string.Empty;
        //    string tmpStr=string.Empty;


        //    if (folderType=="tmp")
        //        requestedFolder = SiteUtils.GetNavigationSiteRoot() + "/Data/Sites/" + siteId + "/amsuserfiles/" + Idd + "/";
        //    else
        //        requestedFolder = SiteUtils.GetNavigationSiteRoot() + "/Data/Sites/" + siteId + "/AutoMEPData/" + Idd + "/";



        //    return requestedFolder;
        //}

        ///// <summary>
        ///// To retrive Save Folder:  current user temp folder to work in Or the current AutoMEP System Data Folder
        ///// </summary>
        ///// <param name="folderType">Either User Temp "tmp" Either AutoMEP System Data "ams" Folder</param> 
        ///// <param name="moduleName">Folder Name; Used Only if requested AutoMEP System Data "ams" Folder </param>
        ///// <param name="siteId"></param>
        ///// <param name="Idd">When AutoMEP System Data Folder "ams" It Will be "AutoMEPID"; When User Temp "tmp" It will be "UserId" </param> 
        ///// <returns>requestedFolder</returns> 
        //public static string GetFolderToSaveIn(string folderType, int siteId, int Idd)//, string moduleName)
        //{
        //    string requestedFolder = string.Empty;
        //    string tmpStr = string.Empty;

        //    //Server.MapPath("\\") + "Data\\Sites\\" + autoMEP.SiteID + "\\userfiles\\" + autoMEP.UserID + "\\"

        //    if (folderType == "tmp")
        //        requestedFolder = "Data\\Sites\\" + siteId + "\\amsuserfiles\\" + Idd + "\\";
        //    else
        //        requestedFolder = "Data\\Sites\\" + siteId + "\\AutoMEPData\\" + Idd + "\\";

        //    return requestedFolder;
        //}

        ///// <summary>
        /////To Create Folders for each new RFP 
        ///// </summary>

        #endregion
    }

    public class EnumUtils
    {
        public static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static object enumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }

       
    }
}
