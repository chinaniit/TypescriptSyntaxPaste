﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TypescriptSyntaxPaste.Tests
{
    [TestClass]
    public class DTOTests
    {
        [TestMethod]
        public void Convert_ClassInterfaceDeclaration_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"
public class ThisClass
{

}

public interface ThisInterface
{

}", @"export class ThisClass {

}
export interface ThisInterface {

}");
        }

        [TestMethod]
        public void Convert_ClassWithBaseClassAndInterface_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"
public class ThisClass : BaseClass, IInterace
{

}", @"
export class ThisClass extends BaseClass implements IInterace {

}");
        }

        [TestMethod]
        public void Convert_InterfaceExtendedInterface_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public interface ThisInterface: IAnotherInterface
{

}", @"export interface ThisInterface extends IAnotherInterface {

}");
        }

        [TestMethod]
        public void Convert_WithGeneric_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class ThisClass<T> : BaseClass<T>, IInterace
{

}", @"export class ThisClass<T> extends BaseClass<T> implements IInterace {

}");
        }

        [TestMethod]
        public void Convert_GenericBaseClassConstrant_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"
public class ThisClass<T> : BaseClass<T>, IInterace where T : ClassConstrant
{

}", @"
export class ThisClass<T extends ClassConstrant> extends BaseClass<T> implements IInterace {

}");
        }

        [TestMethod]
        public void Convert_WithPrimitiveTypes_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"
using System;

public class DTO
{
    public string Name { get; set; }

    public int Age { get; set; }

    public bool IsSingle { get; set; }

    public long Distance { get; set; }

    public DateTime LastUpdate { get; set; }

    public double Money { get; set; }
}
",
@"
export class DTO {
    public name: string;
    public age: number;
    public isSingle: boolean;
    public distance: number;
    public lastUpdate: Date;
    public money: number;
}
");
        }

        [TestMethod]
        public void Convert_Fields_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO
{
    public string Field1;
    public string Field2;
    public string field3;
}", @"export class DTO {
    public field1: string;
    public field2: string;
    public field3: string;
}");
        }

        [TestMethod]
        public void Convert_Modifier_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO
{
    public string Field1;
    private string Field2;
    protected string field3;
    internal string Field4;
    public static string Field5;
}", @"export class DTO {
    public field1: string;
    private field2: string;
    protected field3: string;
    public field4: string;
    public static field5:string;
}");
        }

        [TestMethod]
        public void Convert_AutoProperty_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"
namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

        public partial class CompanyEntity
        {
            public string Name { get; set; }

            public string Address { get; set; }

            public string Phone { get; set; }

            public string Fax { get; set; }

            public string Email { get; set; }
        }
    }

",
@"
module DataAccess {

    export class CompanyEntity         {

            public name: string;

        public address: string;

        public phone: string;

        public fax: string;

        public email: string;
    }
}
");
        }

        [TestMethod]
        public void Convert_GetProperty_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO2
{
    private string field1;
    public string Field1
    {
        get { return field1; }
    }
}", @"export class DTO2 {
    private field1: string;
    public get field1(): string {
        return this.field1;
    }
}");
        }

        [TestMethod]
        public void Convert_SetProperty_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO3
{
    private string field1;
    public string field1
    {
        set { field1 = value; }
    }
}", @"export class DTO3 {
    private field1: string;
    public set field1(value: string) {
        this.field1 = value;
    }
}");
        }

        [TestMethod]
        public void Convert_Method_NoParameter_NoReturnType_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"
public void Method1()
    {
    }", @"
public method1(): void
{

}");
        }

        [TestMethod]
        public void Convert_Method_WithParameter_ReturnType_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public int Method1(int a, string b, string c)
    {

    }", @"public method1(a:number, b:string, c:string): number
{

}");
        }

        [TestMethod]
        public void Convert_Method_WithGeneric_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"
public int Method1<T>(int a, string b, string c)
    {

    }", @"public method1<T>(a:number, b:string, c:string): number
{

}");
        }

        [TestMethod]
        public void Convert_Method_WithGenericConstrant_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public int Method1<T>(int a, string b, string c) where T : BaseClass
    {

    }", @"public method1<T extends BaseClass>(a:number, b:string, c:string): number
{

}");
        }

        [TestMethod]
        public void Convert_DeclareLocalVariable_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@" 
public class DTO5
{
    public void Method()
    {
        int a;
        string b;
        DTO5 e;
    }
}", @"
export class DTO5 {
    public method(): void {
        var a: number;
        var b: string;
        var e: DTO5;
    }
}");
        }

        [TestMethod]
        public void Convert_DeclareLocalVarialeWithAssignment_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO5
{
    public void Method()
    {
        int a = 1;
        string b = ""a"";
        DTO5 e = null;
        }
    }", @"
export class DTO5 {
    public method(): void {
        var a: number = 1;
        var b: string = ""a"";
        var e: DTO5 = null;
        }
    }");
        }

        [TestMethod]
        public void Convert_ClassWithParameterlessConstructor_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO6
{
    public DTO6()
    {

    }
}", @"export class DTO6 {
    constructor() {

    }
}");
        }

        [TestMethod]
        public void Convert_ClassWithConstructor_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO6
{
    public DTO6(int a, string b,object c)
    {

    }
}", @"export class DTO6 {
    constructor(a: number, b: string, c: Object) {

    }
}");
        }

        [TestMethod]
        public void Convert_ObjectCreation_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO5
{
    public void Method()
    {
        DTO5 obj;
        obj = new DTO5();
    }
}", @"export class DTO5 {
    public method(): void {
        var obj: DTO5;
        obj = new DTO5();
    }
}");
        }

        [TestMethod]
        public void Convert_ObjectCreationWithParameters_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO7
{
    public void Method1()
    {
        var a = new DTO6(1,b,c);
    }
}", @"export class DTO7 {
    public method1(): void {
        var a = new DTO6(1, b, c);
    }
}");
        }

        [TestMethod]
        public void Convert_AppendingThisIfItsFields_Correct()
        {
            ConvertHelper.AssertConvertingIgnoreSpaces(@"public class DTO7
{
    private int field = 0;
    public void Method1()
    {
        var localVariable = 1;
        field = 1;
        localVariable = 2;
    }
}", @"export class DTO7 {
    private field: number = 0;
    public method1(): void {
        var localVariable = 1;
        this.field = 1;
        localVariable = 2;
    }
}");
        }
    }
}
