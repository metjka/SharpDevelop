﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Editor.CodeCompletion;
using ICSharpCode.XmlEditor;
using NUnit.Framework;
using XmlEditor.Tests.Utils;

namespace XmlEditor.Tests.Completion
{
	public class NamespaceCompletionWindowTestFixtureBase
	{
		protected MockTextEditor textEditor;
		protected CodeCompletionKeyPressResult keyPressResult;
		protected XmlSchemaCompletionDataCollection schemas;
		protected XmlEditorOptions options;
		
		protected void InitBase()
		{
			schemas = new XmlSchemaCompletionDataCollection();
			AddSchemas();

			options = new XmlEditorOptions(new Properties(), new DefaultXmlSchemaFileAssociations(new AddInTreeNode()), schemas);
			
			textEditor = new MockTextEditor();
			textEditor.Document.Text = "<a xmlns></a>";
			textEditor.FileName = new FileName(@"c:\projects\test.xml");
			
			// Put caret just after "xmlns".
			textEditor.Caret.Offset = 8;			
		}		
		
		void AddSchemas()
		{
			string xml = "<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' targetNamespace='c' />";
			schemas.Add(new XmlSchemaCompletionData(new StringReader(xml)));
			
			xml = "<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' targetNamespace='b' />";
			schemas.Add(new XmlSchemaCompletionData(new StringReader(xml)));
			
			xml = "<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' targetNamespace='a' />";
			schemas.Add(new XmlSchemaCompletionData(new StringReader(xml)));			
		}		
	}
}
