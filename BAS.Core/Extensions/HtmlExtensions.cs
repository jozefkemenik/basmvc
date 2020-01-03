using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BAS.Core.Helper;

namespace BAS.Core.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlString InjectHtmlTag(this HtmlHelper helper, string label, string substring, string tag)
        {
            if (!label.Contains(substring))
            {
                return helper.Raw(label);
            }
            label = label.Insert(label.IndexOf(substring), string.Format("<{0}>", tag));
            label = label.Insert(label.IndexOf(substring) + substring.Length, string.Format("</{0}>", tag));

            return helper.Raw(label);
        }



        public static string RadioButton(this HtmlHelper htmlHelper, string name, SelectListItem listItem,
                         IDictionary<string, object> htmlAttributes)
        {
            var label = new TagBuilder("label");
            label.MergeAttribute("class", "radio");
            var radio = new TagBuilder("input");
            if (listItem.Selected)
            { 
                radio.MergeAttribute("checked", "checked");
            }
            radio.MergeAttribute("type", "radio");
            radio.MergeAttribute("value", listItem.Value);
            radio.MergeAttribute("name", name);
            //builder.MergeAttribute("id", inputIdSb.ToString());
            radio.MergeAttributes(htmlAttributes);
            radio.InnerHtml = listItem.Text;
            label.InnerHtml = radio.ToString(TagRenderMode.Normal);
            return label.ToString(TagRenderMode.Normal);
        }





        public static IHtmlString EnumCheckboxListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression,  object htmlAttributes)
        {
            var htmlAttributesList = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            
            string name = GetDropDownListName(htmlHelper, expression, htmlAttributesList);
            IEnumerable<SelectListItem> selectList = htmlHelper.GetSelectListItemsFor<TModel, TEnum>(expression);

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var divTag = new TagBuilder("div");

            // Add the validation attributes
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                    divTag.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
            divTag.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));


            divTag.MergeAttribute("class", "radio");
          
            
     
            foreach (var item in selectList)
            {
                var radioButtonTag = RadioButton(htmlHelper, name, item, htmlAttributesList);
                divTag.InnerHtml += radioButtonTag;
            }

            return new HtmlString(divTag.ToString());
        }
            
        #region EnumDropDownList

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return EnumDropDownListFor(htmlHelper, expression, null /* optionLabel */, null /* htmlAttributes */);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return EnumDropDownListFor(htmlHelper, expression, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IDictionary<string, object> htmlAttributes)
        {
            return EnumDropDownListFor(htmlHelper, expression, null /* optionLabel */, htmlAttributes);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel)
        {
            return EnumDropDownListFor(htmlHelper, expression, optionLabel, null /* htmlAttributes */);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
        {
            return EnumDropDownListFor(htmlHelper, expression, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            string name = GetDropDownListName(htmlHelper, expression, htmlAttributes);
            var selectList = htmlHelper.GetSelectListItemsFor<TModel, TEnum>(expression).ToList();
            //MvcHtmlString htmlString = htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);


            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var select = new TagBuilder("select");

             // Add the validation attributes
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                    select.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
            select.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));


           
            select.MergeAttribute("name", name);
            select.MergeAttribute("id", name);
            select.MergeAttributes(htmlAttributes);

            if (!String.IsNullOrEmpty(optionLabel))
            {
                selectList.Insert(0, new SelectListItem()
                {
                    Selected = false,
                    Text = optionLabel,
                }
                );
            
            }

            foreach (var item in selectList)
            {
                var option = new TagBuilder("option");
                option.MergeAttribute("value", item.Value);
                 if (item.Selected)
                 { 
                     option.MergeAttribute("selected", "selected");
                 }
                 option.InnerHtml = item.Text;
                select.InnerHtml+=option.ToString(TagRenderMode.Normal);
            }

            MvcHtmlString htmlString = new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            
        




               // DropDownList(name, selectList, optionLabel, htmlAttributes);
            return htmlString;
        }





        //public static MvcHtmlString ValidationMessageInLoopFor<TModel, IItem, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IItem item)
        //{
        //    var model = item as IInLoopForModel;
        //    if (model == null)
        //        return MvcHtmlString.Empty;

        //    var fieldName = ExpressionHelper.GetExpressionText(expression);
        //    var name = model.Name + fieldName;

        //    var tag = new TagBuilder("span");
        //    tag.Attributes.Add("class", "field-validation-valid");
        //    tag.Attributes.Add("data-valmsg-replace", "true");
        //    tag.Attributes.Add("data-valmsg-for", name);
        //    return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        //}



        #region Private methods

        private static string GetDropDownListName<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IDictionary<string, object> htmlAttributes = null)
        {
            string nameFromExpression = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            if (htmlAttributes == null)
                return nameFromExpression;

            // dropdownlist name:
            // if htmlAttributes["name"] exists, uses attribute 
            // if none of the previous attributes exists, uses name from lambda expression
            string name = htmlAttributes.ContainsKey("name") ?
                htmlAttributes["name"].ToString() :
                nameFromExpression;

            return name;
        }

        private static IEnumerable<SelectListItem> GetSelectListItemsFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            bool isEnum = EnumHelper.IsEnum<TEnum>();
            if (!isEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            IEnumerable<SelectListItem> values = EnumHelper.GetEnumValues<TEnum>().Select(x =>
                new SelectListItem
                {
                    Text = x.Value,
                    Value = Convert.ToInt32(x.Key).ToString(),
                    Selected = x.Key.Equals(metadata.Model)
                }
            );

            return values;
        }
        #endregion
        #endregion





    }
}
