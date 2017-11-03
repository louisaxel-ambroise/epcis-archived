using FasTnT.Domain.Exceptions;
using FasTnT.Domain.Model;
using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Model.Queries;
using System;
using System.Linq;

namespace FasTnT.Domain.Services.EventQuery
{
    public static class SimpleEventQueryParameters
    {
        public static IQueryable<EpcisEvent> ApplyParameter(IQueryable<EpcisEvent> query, QueryParam param)
        {
            switch (param.Name)
            {
                case "eventType":
                    return query.Where(e => e.EventType == (EventType)Enum.Parse(typeof(EventType), param.Value));
                case "GE_eventTime":
                    return query.Where(e => e.EventTime >= DateTime.Parse(param.Value));
                case "LT_eventTime":
                    return query.Where(e => e.EventTime < DateTime.Parse(param.Value));
                case "GE_recordTime":
                    return query.Where(e => e.CaptureTime >= DateTime.Parse(param.Value));
                case "LT_recordTime":
                    return query.Where(e => e.CaptureTime < DateTime.Parse(param.Value));
                case "EQ_action":
                    return query.Where(e => e.Action == (EventAction)Enum.Parse(typeof(EventAction), param.Value));
                case "EQ_bizStep":
                    return query.Where(e => param.Values.Contains(e.BusinessStep));
                case "EQ_disposition":
                    return query.Where(e => param.Values.Contains(e.Disposition));
                case "EQ_readPoint":
                    return query.Where(e => param.Values.Contains(e.ReadPoint));
                case "EQ_bizLocation":
                    return query.Where(e => param.Values.Contains(e.BusinessLocation));
                case "EQ_bizTransaction_type":
                    return query.Where(e => e.BusinessTransactions.Any(tx => param.Values.Contains(tx.Type)));
                case "EQ_source_type":
                    return query.Where(e => e.SourcesDestinations.Any(tx => tx.Direction == SourceDestinationType.Source && param.Values.Contains(tx.Type)));
                case "EQ_destination_type":
                    return query.Where(e => e.SourcesDestinations.Any(tx => tx.Direction == SourceDestinationType.Destination && param.Values.Contains(tx.Type)));
                case "EQ_transformationID":
                    return query.Where(e => param.Values.Contains(e.TransformationId));
                case "MATCH_epc":
                    return query.Where(e => e.Epcs.Any(epc => (epc.Type == EpcType.List || epc.Type == EpcType.ChildEpc) && param.Values.Contains(epc.Id)));
                case "MATCH_parentID":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.ParentId && param.Values.Contains(epc.Id)));
                case "MATCH_inputEPC":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.InputEpc && param.Values.Contains(epc.Id)));
                case "MATCH_outputEPC":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.OutputEpc && param.Values.Contains(epc.Id)));
                case "MATCH_anyEPC":
                    return query.Where(e => e.Epcs.Any(epc => param.Values.Contains(epc.Id)));
                case "MATCH_epcClass":
                    return query.Where(e => e.Epcs.Any(epc => new[] { EpcType.InputQuantity, EpcType.OutputQuantity, EpcType.Quantity }.Contains(epc.Type) && param.Values.Contains(epc.Id)));
                case "MATCH_inputEPCClass":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.InputQuantity && param.Values.Contains(epc.Id)));
                case "MATCH_outputEPCClass":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.OutputQuantity && param.Values.Contains(epc.Id)));
                case "MATCH_anyEPCClass":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.OutputQuantity && param.Values.Contains(epc.Id)));
                case "EQ_quantity":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.Quantity && epc.Quantity == int.Parse(param.Value)));
                case "GT_quantity":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.Quantity && epc.Quantity > int.Parse(param.Value)));
                case "GE_quantity":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.Quantity && epc.Quantity >= int.Parse(param.Value)));
                case "LT_quantity":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.Quantity && epc.Quantity < int.Parse(param.Value)));
                case "LE_quantity":
                    return query.Where(e => e.Epcs.Any(epc => epc.Type == EpcType.Quantity && epc.Quantity <= int.Parse(param.Value)));
                // TODO: a number of parameters are missing. Specification 1.2 29/09/2016 pp. 74-79
                case "EXISTS_errorDeclaration":
                    return query.Where(e => e.ErrorDeclaration != null);
                case "GE_errorDeclaration_Time":
                    return query.Where(e => e.ErrorDeclaration != null && e.ErrorDeclaration.DeclarationTime >= DateTime.Parse(param.Value));
                case "LT_errorDeclaration_Time":
                    return query.Where(e => e.ErrorDeclaration != null && e.ErrorDeclaration.DeclarationTime < DateTime.Parse(param.Value));
                case "EQ_errorReason":
                    return query.Where(e => e.ErrorDeclaration != null && param.Values.Contains(e.ErrorDeclaration.Reason));
                // Limits and take
                case "eventCountLimit":
                    return query.Take(int.Parse(param.Value));
                case "maxEventCount":
                    return query.Take(int.Parse(param.Value) + 1); // Don't try to load the entire DB. If number = this max value, then throw an exception
                case "orderBy":
                case "orderDirection":
                    throw new ImplementationException(param.Name);
                default:
                    return ApplyFieldNameQuery(query, param);
            }
        }

        // Custom Fields query can be of 3 types: text (can contain multiple values, return if any matches), numeric and DateTime.
        private static IQueryable<EpcisEvent> ApplyFieldNameQuery(IQueryable<EpcisEvent> query, QueryParam param)
        {
            var fieldName = param.Name.Split('_').Last().Split('#');
            var nameSpace = fieldName[0];
            var name = fieldName[1];

            if (param.Name.StartsWith("EQ_ILMD")) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && param.Values.Contains(f.TextValue)));
            if (param.Name.StartsWith("GT_ILMD") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.DateValue > (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("GT_ILMD") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.NumericValue > (double)param.ComparableValue));
            if (param.Name.StartsWith("GE_ILMD") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.DateValue >= (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("GE_ILMD") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.NumericValue >= (double)param.ComparableValue));
            if (param.Name.StartsWith("LT_ILMD") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.DateValue < (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("LT_ILMD") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.NumericValue < (double)param.ComparableValue));
            if (param.Name.StartsWith("LE_ILMD") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.DateValue <= (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("LE_ILMD") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.NumericValue <= (double)param.ComparableValue));

            if (param.Name.StartsWith("EQ_INNER_ILMD")) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && param.Values.Contains(f.TextValue)));
            if (param.Name.StartsWith("GT_INNER_ILMD") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.DateValue > (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("GT_INNER_ILMD") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.NumericValue > (double)param.ComparableValue));
            if (param.Name.StartsWith("GE_INNER_ILMD") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.DateValue >= (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("GE_INNER_ILMD") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.NumericValue >= (double)param.ComparableValue));
            if (param.Name.StartsWith("LT_INNER_ILMD") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.DateValue < (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("LT_INNER_ILMD") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.NumericValue < (double)param.ComparableValue));
            if (param.Name.StartsWith("LE_INNER_ILMD") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.DateValue <= (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("LE_INNER_ILMD") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.Ilmd && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.NumericValue <= (double)param.ComparableValue));

            if (param.Name.StartsWith("EQ_INNER_")) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && param.Values.Contains(f.TextValue)));
            if (param.Name.StartsWith("GT_") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.DateValue > (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("GT_") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.NumericValue > (double)param.ComparableValue));
            if (param.Name.StartsWith("GE_") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.DateValue >= (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("GE_") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.NumericValue >= (double)param.ComparableValue));
            if (param.Name.StartsWith("LT_") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.DateValue < (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("LT_") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.NumericValue < (double)param.ComparableValue));
            if (param.Name.StartsWith("LE_") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.DateValue <= (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("LE_") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId != null && f.Namespace == nameSpace && f.Name == name && f.NumericValue <= (double)param.ComparableValue));

            if (param.Name.StartsWith("EQ_")) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && param.Values.Contains(f.TextValue)));
            if (param.Name.StartsWith("GT_") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.DateValue > (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("GT_") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.NumericValue > (double)param.ComparableValue));
            if (param.Name.StartsWith("GE_") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.DateValue >= (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("GE_") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.NumericValue >= (double)param.ComparableValue));
            if (param.Name.StartsWith("LT_") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.DateValue < (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("LT_") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.NumericValue < (double)param.ComparableValue));
            if (param.Name.StartsWith("LE_") && param.ComparableType == typeof(DateTime)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.DateValue <= (DateTime)param.ComparableValue));
            if (param.Name.StartsWith("LE_") && param.ComparableType == typeof(double)) return query.Where(e => e.CustomFields.Any(f => f.Type == FieldType.EventExtension && f.ParentId == null && f.Namespace == nameSpace && f.Name == name && f.NumericValue <= (double)param.ComparableValue));

            throw new QueryParameterException(param.Name);
        }
    }
}
