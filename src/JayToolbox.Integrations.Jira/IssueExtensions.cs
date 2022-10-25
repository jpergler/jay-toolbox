using System;
using System.Linq;
using Atlassian.Jira;

namespace JayToolbox.Integrations.Jira;

public static class IssueExtensions
{
    public static T? GetCustomField<T>(this Issue issue, string fieldName)
    {
        var value = issue.CustomFields[fieldName]?.Values.FirstOrDefault();
        if (value == null) return default;
        return (T?)Convert.ChangeType(value, typeof(T));
    }
}