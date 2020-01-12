// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.TimeSeriesInsights.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Get Series query. Allows to retrieve time series of calculated variable
    /// values from events for a given Time Series ID and search span.
    /// </summary>
    public partial class GetSeries
    {
        private object filter;
        private Dictionary<string, NumericVariable> inlineVariables;

        /// <summary>
        /// Initializes a new instance of the GetSeries class.
        /// </summary>
        public GetSeries()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the GetSeries class.
        /// </summary>
        /// <param name="timeSeriesId">A single Time Series ID value that
        /// uniquely identifies a single time series instance (e.g. a device).
        /// Note that a single Time Series ID can be composite if multiple
        /// properties are specified as Time Series ID at environment creation
        /// time. The position and type of values must match Time Series ID
        /// properties specified on the environment and returned by Get Model
        /// Setting API. Cannot be null.</param>
        /// <param name="searchSpan">The range of time on which the query is
        /// executed. Cannot be null.</param>
        /// <param name="filter">Top-level filter over the events that
        /// restricts the number of events being considered for computation.
        /// This filter is AND'ed with filter in each variable. Example:
        /// "$event.Status.String='Good'". Optional.</param>
        /// <param name="projectedVariables">Selected variables that needs to
        /// be projected in the query result. When it is null or not set, all
        /// the variables from inlineVariables and time series type in the
        /// model are returned. Can be null.</param>
        /// <param name="inlineVariables">Optional inline variables apart from
        /// the ones already defined in the time series type in the model. When
        /// the inline variable name is the same name as in the model, the
        /// inline variable definition takes precedence. Can be null.</param>
        public GetSeries(IList<object> timeSeriesId, DateTimeRange searchSpan, Tsx filter = default(Tsx), IList<string> projectedVariables = default(IList<string>), IDictionary<string, Variable> inlineVariables = default(IDictionary<string, Variable>))
        {
            TimeSeriesId = timeSeriesId;
            SearchSpan = searchSpan;
            Filter = filter;
            ProjectedVariables = projectedVariables;
            InlineVariables = inlineVariables;
            CustomInit();
        }

        public GetSeries(object[] timeSeriesId, DateTimeRange searchSpan, object filter, string[] projectedVariables, Dictionary<string, NumericVariable> inlineVariables)
        {
            TimeSeriesId = timeSeriesId;
            SearchSpan = searchSpan;
            this.filter = filter;
            ProjectedVariables = projectedVariables;
            this.inlineVariables = inlineVariables;
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets a single Time Series ID value that uniquely identifies
        /// a single time series instance (e.g. a device). Note that a single
        /// Time Series ID can be composite if multiple properties are
        /// specified as Time Series ID at environment creation time. The
        /// position and type of values must match Time Series ID properties
        /// specified on the environment and returned by Get Model Setting API.
        /// Cannot be null.
        /// </summary>
        [JsonProperty(PropertyName = "timeSeriesId")]
        public IList<object> TimeSeriesId { get; set; }

        /// <summary>
        /// Gets or sets the range of time on which the query is executed.
        /// Cannot be null.
        /// </summary>
        [JsonProperty(PropertyName = "searchSpan")]
        public DateTimeRange SearchSpan { get; set; }

        /// <summary>
        /// Gets or sets top-level filter over the events that restricts the
        /// number of events being considered for computation. This filter is
        /// AND'ed with filter in each variable. Example:
        /// "$event.Status.String='Good'". Optional.
        /// </summary>
        [JsonProperty(PropertyName = "filter")]
        public Tsx Filter { get; set; }

        /// <summary>
        /// Gets or sets selected variables that needs to be projected in the
        /// query result. When it is null or not set, all the variables from
        /// inlineVariables and time series type in the model are returned. Can
        /// be null.
        /// </summary>
        [JsonProperty(PropertyName = "projectedVariables")]
        public IList<string> ProjectedVariables { get; set; }

        /// <summary>
        /// Gets or sets optional inline variables apart from the ones already
        /// defined in the time series type in the model. When the inline
        /// variable name is the same name as in the model, the inline variable
        /// definition takes precedence. Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "inlineVariables")]
        public IDictionary<string, Variable> InlineVariables { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (TimeSeriesId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TimeSeriesId");
            }
            if (SearchSpan == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "SearchSpan");
            }
            if (SearchSpan != null)
            {
                SearchSpan.Validate();
            }
            if (Filter != null)
            {
                Filter.Validate();
            }
            if (InlineVariables != null)
            {
                foreach (var valueElement in InlineVariables.Values)
                {
                    if (valueElement != null)
                    {
                        valueElement.Validate();
                    }
                }
            }
        }
    }
}
