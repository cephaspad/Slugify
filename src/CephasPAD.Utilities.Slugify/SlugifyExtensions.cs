using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CephasPAD.Utilities.Slugify
{
	// Token: 0x02000003 RID: 3
	public static partial class SlugifyExtensions
	{
		/// <summary>
		/// Generate slug string
		/// </summary>
		/// <param name="incomingString"></param>
		/// <param name="slugSeparator"></param>
		/// <returns></returns>
		public static string Slugify(this string incomingString, string slugSeparator = "-")
		{
			incomingString = incomingString.Unidecode(null);
			return Regex.Replace(Regex.Replace(incomingString, "[^a-zA-Z0-9\\s]", string.Empty), "\\s+", slugSeparator).ToLower();
		}

		/// <summary>
		/// Generate slug string
		/// </summary>
		/// <param name="incomingString"></param>
		/// <param name="items"></param>
		/// <param name="slugSeparator"></param>
		/// <returns></returns>
		public static string GenerateUniqueSlug<T>(this string incomingString, List<T> items, string slugSeparator = "-") where T : ISlug
		{
			string slug = incomingString.Slugify(slugSeparator);
			if (items.Any((T m) => m.Slug == slug))
			{
				slug += slug.GetHashCode();
				if (items.Any((T m) => m.Slug == slug))
				{
					slug += Guid.NewGuid();
				}
			}
			return slug;
		}

		/// <summary>
		/// Generate slug string
		/// </summary>
		/// <param name="incomingString"></param>
		/// <param name="items"></param>
		/// <param name="expression"></param>
		/// <param name="slugSeparator"></param>
		/// <returns></returns>
		public static string UniqueSlugify<TSource>(this string incomingString, List<TSource> items, Expression<Func<TSource, string>> expression, string slugSeparator = "-")
		{
			PropertyInfo propInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
			string slug = incomingString.Slugify(slugSeparator);
			if (items.Any(delegate(TSource m)
			{
				string a;
				if (propInfo == null)
				{
					a = null;
				}
				else
				{
					object value = propInfo.GetValue(m);
					a = ((value != null) ? value.ToString() : null);
				}
				return a == slug;
			}))
			{
				slug += slug.GetHashCode();
				if (items.Any(delegate(TSource m)
				{
					string a;
					if (propInfo == null)
					{
						a = null;
					}
					else
					{
						object value = propInfo.GetValue(m);
						a = ((value != null) ? value.ToString() : null);
					}
					return a == slug;
				}))
				{
					slug += Guid.NewGuid();
				}
			}
			return slug;
		}
	}
}
