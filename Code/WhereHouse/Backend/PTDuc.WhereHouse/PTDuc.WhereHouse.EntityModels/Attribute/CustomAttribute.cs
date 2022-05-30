using System;
using System.Reflection;

namespace PTDuc.WhereHouse.EntityModels.Attribute
{
    /// <summary>
    /// attribute cho các trường bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : FlagsAttribute
    {
    }
    /// <summary>
    /// attribute cho các trường phải check trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : FlagsAttribute
    {
    }
    /// <summary>
    /// attribute cho các trường là khóa chinh
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : FlagsAttribute
    {

    }
    /// <summary>
    /// attribute cho các trường là email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Email : FlagsAttribute
    {

    }
    /// <summary>
    /// attribute cho các trường yêu cầu độ dài tối đa
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : FlagsAttribute
    {
        public int Value { get; set; }
        public string ErrorMsg { get; set; }
        public MaxLength(int length, string errorMsg)
        {
            this.Value = length;
            this.ErrorMsg = errorMsg;
        }
    }
    /// <summary>
    /// attribute cho các trường là email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayNameAttribute : FlagsAttribute { 
    }
}
