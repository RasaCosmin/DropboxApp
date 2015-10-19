using System;
using Java.Sql;
using System.Collections.Generic;

namespace DropboxApp
{
	public class GetResponse
	{
		public List<LabTestsUserOnlyResult> LabTestsUserOnlyResult{ get; set; }
	}

	public class LabTestsUserOnlyResult
	{
		public Category Category { get; set; }

		public List<LabTestPanels> LabTestPanels{ get; set; }

		public string Description { get; set; }

		public string FullName { get; set; }

		public string Id { get; set; }

		public string LangCode{ get; set; }

		public string Name { get; set; }

		public string Unit { get; set; }

		public string RefValue { get; set; }

		public LatestLabTestResult LatestLabTestResult{ get; set; }
	
	}

	public class Category
	{
		public string Id { get; set; }

		public string Name{ get; set; }

		public string LangCode{ get; set; }
	}

	public class LabTestPanels
	{
		public string FullName{ get; set; }

		public string Id { get; set; }

		public string LangCode{ get; set; }

		public string Name{ get; set; }
	}

	public class LatestLabTestResult
	{
		public string Id{ get; set; }

		public string RecordDate{ get; set; }

		public int Result{ get; set; }

		public float Value{ get; set; }

		public List<string> ResultOptions { get; set; }
	}
}

