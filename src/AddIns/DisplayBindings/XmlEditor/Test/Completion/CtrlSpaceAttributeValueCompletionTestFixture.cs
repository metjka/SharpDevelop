﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Editor.CodeCompletion;
using ICSharpCode.XmlEditor;
using NUnit.Framework;
using XmlEditor.Tests.Utils;

namespace XmlEditor.Tests.Completion
{
	[TestFixture]
	public class CtrlSpaceAttributeValueCompletionTestFixture
	{
		bool result;
		MockTextEditor textEditor;
		XmlCodeCompletionBinding completionBinding;
		XmlEditorOptions options;
		XmlSchemaCompletionData xsdSchema;
		
		[SetUp]
		public void Init()
		{
			XmlSchemaCompletionDataCollection schemas = new XmlSchemaCompletionDataCollection();
			xsdSchema = new XmlSchemaCompletionData(ResourceManager.GetXsdSchema());
			schemas.Add(xsdSchema);

			options = new XmlEditorOptions(new Properties(), new DefaultXmlSchemaFileAssociations(new AddInTreeNode()), schemas);
			options.SetSchemaFileAssociation(new XmlSchemaFileAssociation(".xsd", "http://www.w3.org/2001/XMLSchema", "xs"));
			
			textEditor = new MockTextEditor();
			textEditor.FileName = new FileName(@"c:\projects\test.xsd");
			textEditor.Document.Text = "<xs:schema elementFormDefault=\"\"></xs:schema>";
			
			// Put cursor inside the double quotes following the elementFormDefault attribute
			textEditor.Caret.Offset = 31;	
			
			completionBinding = new XmlCodeCompletionBinding(options);
			result = completionBinding.CtrlSpace(textEditor);
		}
		
		[Test]
		public void CtrlSpaceMethodResultIsTrueWhenCursorIsInsideAttributeValue()
		{
			Assert.IsTrue(result);
		}
		
		[Test]
		public void CtrlSpaceMethodResultIsFalseWhenCursorIsOutsideAttributeValue()
		{
			textEditor.Caret.Offset = 0;
			Assert.IsFalse(completionBinding.CtrlSpace(textEditor));
		}
		
		[Test]
		public void ShowCompletionWindowCalledWithCompletionItems()
		{
			ICompletionItem[] items = textEditor.CompletionItemsDisplayedToArray();
			ICompletionItem[] expectedItems = GetXsdSchemaElementFormDefaultAttributeValues();
			
			Assert.AreEqual(expectedItems, items);
		}
		
		ICompletionItem[] GetXsdSchemaElementFormDefaultAttributeValues()
		{
			XmlElementPath path = new XmlElementPath();
			path.Elements.Add(new QualifiedName("schema", "http://www.w3.org/2001/XMLSchema", "xs"));
			return xsdSchema.GetAttributeValueCompletionData(path, "elementFormDefault");
		}
		
		[Test]
		public void ElementFormDefaultAttributeHasAttributeValues()
		{
			Assert.IsTrue(GetXsdSchemaElementFormDefaultAttributeValues().Length > 0);
		}
	}
}
