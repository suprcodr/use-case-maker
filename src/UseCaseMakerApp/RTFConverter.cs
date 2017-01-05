using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Collections.Specialized;
using System.Net;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.rtf;
using iTextSharp.text.xml;
using iTextSharp.text.rtf.headerfooter;

namespace UseCaseMaker
{
	/**
	 * @brief Descrizione di riepilogo per PDFConverter.
	 */
	public class RTFConverter
	{
		#region Enumerators and Constants
		// Public
		// Private
		// Protected
		#endregion

		#region Class Members
		// Public
		// Private
		private string stylesheetFilesPath = string.Empty;
		private string rtfFilesPath = string.Empty;
		private Localizer localizer = null;
		// Protected
		#endregion

		#region Constructors
		/**
		 * @brief Costruttore di default per XMIConverter
		 */
		public RTFConverter(
			string stylesheetFilesPath,
			string rtfFilesPath,
			Localizer localizer)
		{
			this.stylesheetFilesPath = stylesheetFilesPath;
			this.rtfFilesPath = rtfFilesPath;
			this.localizer = localizer;
		}
		#endregion

		#region Public Properties
		#endregion

		#region Private Properties
		#endregion

		#region Protected Properties
		#endregion

		#region Public Methods
		public void Transform(string modelFilePath)
		{
			StreamReader sr;
			string foDoc;
			MemoryStream ms = new MemoryStream();
			XmlResolver resolver = new XmlUrlResolver();
			resolver.Credentials = CredentialCache.DefaultCredentials;
			XmlTextReader doc = new XmlTextReader(modelFilePath);
			XslCompiledTransform transform = new XslCompiledTransform();
			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + "PdfRtfExport.xsl",null,resolver);

			XsltArgumentList al = new XsltArgumentList();
			AssemblyName an = this.GetType().Assembly.GetName();
			al.AddParam("version","",an.Version.ToString(3));
			al.AddParam("outputType","","withoutLink");
			al.AddParam("description","",this.localizer.GetValue("Globals","Description"));
			al.AddParam("notes","",this.localizer.GetValue("Globals","Notes"));
			al.AddParam("relatedDocs","",this.localizer.GetValue("Globals","RelatedDocuments"));
			al.AddParam("model","",this.localizer.GetValue("Globals","Model"));
			al.AddParam("actor","",this.localizer.GetValue("Globals","Actor"));
			al.AddParam("goals","",this.localizer.GetValue("Globals","Goals"));
			al.AddParam("useCase","",this.localizer.GetValue("Globals","UseCase"));
			al.AddParam("package","",this.localizer.GetValue("Globals","Package"));
			al.AddParam("actors","",this.localizer.GetValue("Globals","Actors"));
			al.AddParam("useCases","",this.localizer.GetValue("Globals","UseCases"));
			al.AddParam("packages","",this.localizer.GetValue("Globals","Packages"));
			al.AddParam("summary","",this.localizer.GetValue("Globals","Summary"));
			al.AddParam("glossary","",this.localizer.GetValue("Globals","Glossary"));
			al.AddParam("glossaryItem","",this.localizer.GetValue("Globals","GlossaryItem"));
            al.AddParam("stakeholders", "", this.localizer.GetValue("Globals", "Stakeholders"));
            al.AddParam("stakeholder", "", this.localizer.GetValue("Globals", "Stakeholder"));
			al.AddParam("preconditions","",this.localizer.GetValue("Globals","Preconditions"));
			al.AddParam("postconditions","",this.localizer.GetValue("Globals","Postconditions"));
			al.AddParam("openIssues","",this.localizer.GetValue("Globals","OpenIssues"));
			al.AddParam("flowOfEvents","",this.localizer.GetValue("Globals","FlowOfEvents"));
			al.AddParam("prose","",this.localizer.GetValue("Globals","Prose"));
			al.AddParam("details","",this.localizer.GetValue("Globals","Details"));
			al.AddParam("priority","",this.localizer.GetValue("Globals","Priority"));
			al.AddParam("status","",this.localizer.GetValue("Globals","Status"));
			al.AddParam("level","",this.localizer.GetValue("Globals","Level"));
			al.AddParam("complexity","",this.localizer.GetValue("Globals","Complexity"));
			al.AddParam("implementation","",this.localizer.GetValue("Globals","Implementation"));
			al.AddParam("assignedTo","",this.localizer.GetValue("Globals","AssignedTo"));
			al.AddParam("release","",this.localizer.GetValue("Globals","Release"));
			al.AddParam("activeActors","",this.localizer.GetValue("Globals","ActiveActors"));
			al.AddParam("primary","",this.localizer.GetValue("Globals","Primary"));
			al.AddParam("history","",this.localizer.GetValue("Globals","History"));
			al.AddParam("statusNodeSet","",this.localizer.GetNodeSet("cmbStatus","Item"));
			al.AddParam("levelNodeSet","",this.localizer.GetNodeSet("cmbLevel","Item"));
			al.AddParam("complexityNodeSet","",this.localizer.GetNodeSet("cmbComplexity","Item"));
			al.AddParam("implementationNodeSet","",this.localizer.GetNodeSet("cmbImplementation","Item"));
			al.AddParam("historyTypeNodeSet","",this.localizer.GetNodeSet("HistoryType","Item"));
            al.AddParam("eventTypeNodeSet", "", this.localizer.GetNodeSet("cmbTriggerEvent", "Item"));
            al.AddParam("triggerEvent", "", this.localizer.GetValue("Globals", "TriggerEvent"));
            al.AddParam("triggerDescription", "", this.localizer.GetValue("Globals", "TriggerDescription"));
            al.AddParam("categoryNodeSet", "", this.localizer.GetNodeSet("cmbCategory", "Item"));
            al.AddParam("importanceNodeSet", "", this.localizer.GetNodeSet("cmbImportance", "Item"));
            al.AddParam("acceptanceNodeSet", "", this.localizer.GetNodeSet("cmbAcceptanceStatus", "Item"));
            al.AddParam("proposedBy", "", this.localizer.GetValue("Globals", "ProposedBy"));
            al.AddParam("benefitTo", "", this.localizer.GetValue("Globals", "BenefitTo"));
            al.AddParam("category", "", this.localizer.GetValue("Globals", "Category"));
            al.AddParam("importance", "", this.localizer.GetValue("Globals", "Importance"));
            al.AddParam("acceptance", "", this.localizer.GetValue("Globals", "Acceptance"));
            al.AddParam("mappedUCs", "", this.localizer.GetValue("Globals", "MappedUCs"));
            al.AddParam("requirements", "", this.localizer.GetValue("Globals", "Requirements"));
            al.AddParam("author", "", this.localizer.GetValue("Globals", "Author"));
            al.AddParam("company", "", this.localizer.GetValue("Globals", "Company"));
            al.AddParam("creationDate", "", this.localizer.GetValue("Globals", "CreationDate"));
            al.AddParam("exportPrintDate", "", this.localizer.GetValue("Globals", "ExportPrintDate"));
            al.AddParam("now", "", Convert.ToString(DateTime.Now, DateTimeFormatInfo.InvariantInfo));

			transform.Transform(doc,al,new XmlTextWriter(ms,Encoding.UTF8),resolver);
			ms.Position = 0;
			sr = new StreamReader(ms,Encoding.UTF8);
			foDoc = sr.ReadToEnd();
			sr.Close();
			ms.Close();

			this.XmlToRtf(foDoc,this.rtfFilesPath);
		}
		#endregion

		#region Private Methods
		public void XmlToRtf(string xmlDoc, string strFilename) 
		{        
			Document document = new Document();
			MemoryStream ms = new MemoryStream();
			Phrase headerPhrase;
			Phrase footerPhrase;
        
			// iTextSharp
			RtfWriter2 writer = RtfWriter2.GetInstance(document, ms);

            footerPhrase = new Phrase("",new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA,8));
            RtfHeaderFooter footer = new RtfHeaderFooter(footerPhrase);
            footer.SetAlignment("center");
            writer.Footer = footer;
			
			AssemblyName an = this.GetType().Assembly.GetName();
			headerPhrase = new Phrase(
				"Use Case Maker " + an.Version.ToString(3),
				new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA,8));
			RtfHeaderFooter header = new RtfHeaderFooter(headerPhrase);
			header.SetAlignment("right");
			writer.Header = header;

			StringReader sr = new StringReader(xmlDoc);
			XmlTextReader reader = new XmlTextReader(sr);
			ITextHandler xmlHandler = new ITextHandler(document);

			try
			{
				xmlHandler.Parse(reader);
			}
			catch(Exception e)
			{
				ms.Close();
				throw e;
			}
			finally
			{
				reader.Close();
				sr.Close();
			}

			//Write output file
			FileStream fs = new FileStream(strFilename, FileMode.Create);
			BinaryWriter bw = new BinaryWriter(fs);
			bw.Write(ms.ToArray());
			bw.Close();
			fs.Close();
			ms.Close();
		}
		#endregion

		#region Protected Methods
		#endregion
	}
}

