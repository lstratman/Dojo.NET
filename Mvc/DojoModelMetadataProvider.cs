using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Dojo.Net.Mvc
{
    public class DojoModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            ModelMetadata metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType,
                                                         propertyName);
            RegularExpressionAttribute regularExpressionAttribute =
                attributes.OfType<RegularExpressionAttribute>().FirstOrDefault();
            RangeAttribute rangeAttribute = attributes.OfType<RangeAttribute>().FirstOrDefault();
            CurrencyAttribute currencyAttribute = attributes.OfType<CurrencyAttribute>().FirstOrDefault();
            StringLengthAttribute stringLengthAttribute = attributes.OfType<StringLengthAttribute>().FirstOrDefault();
            MaxLengthAttribute maxLengthAttribute = attributes.OfType<MaxLengthAttribute>().FirstOrDefault();

            if (regularExpressionAttribute != null)
                metadata.AdditionalValues["RegularExpression"] = regularExpressionAttribute;

            if (rangeAttribute != null)
                metadata.AdditionalValues["Range"] = rangeAttribute;

            if (currencyAttribute != null)
                metadata.AdditionalValues["Currency"] = currencyAttribute;

            if (stringLengthAttribute != null && maxLengthAttribute == null && stringLengthAttribute.MaximumLength > 0)
                metadata.AdditionalValues["MaxLength"] = stringLengthAttribute.MaximumLength;

            if (maxLengthAttribute != null)
                metadata.AdditionalValues["MaxLength"] = maxLengthAttribute.Length;

            return metadata;
        }
    }
}