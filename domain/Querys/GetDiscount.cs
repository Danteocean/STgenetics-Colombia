using Dapper;
using Domain.Entities;
using Npgsql;

namespace Domain.Querys;

public class GetDiscount
{
    private String _conexion = "";

    private const String queryDiscountRule = @"SELECT discount_rule_id discountRuleId, name, discount_percent discountPercent FROM discount_rule WHERE is_active = true";
    private const String queryDiscountRuleCategory = @"SELECT discount_rule_category_id discountRuleCategoryId, discount_rule_id discountRuleId, category_id categoryId 
        FROM discount_rule_category WHERE discount_rule_id = ANY(@ruleIds)";

    public GetDiscount(String conexion)
    {
        _conexion = conexion;
    }

    public async Task<List<DiscountRule>> GetActiveRules()
    {
        try
        {
            using var db = new NpgsqlConnection(_conexion);
            var rules = (await db.QueryAsync<DiscountRule>(queryDiscountRule)).ToList();
            return rules;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error GetActiveRules: " + ex.Message);
            return new List<DiscountRule>();
        }
      
    }

    public async Task<List<DiscountRuleCategory>> GetCategoriesByRuleIds(List<int> ruleIds)
    {
        try
        {
            if (ruleIds == null || ruleIds.Count == 0) return new List<DiscountRuleCategory>();
            using var db = new NpgsqlConnection(_conexion);
            var ruleCategories = (await db.QueryAsync<DiscountRuleCategory>(queryDiscountRuleCategory,
                new { ruleIds = ruleIds.ToArray() })).ToList();
            return ruleCategories;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error GetCategoriesByRuleIds: " + ex.Message);
            return new List<DiscountRuleCategory>();
      
        }
     
    }

    public async Task<(List<DiscountRule> Rules, List<DiscountRuleCategory> RuleCategories)> GetActiveRulesWithCategories()
    {
        try
        {
            var rules = await GetActiveRules();

            var ruleCategories = rules.Count > 0
                ? await GetCategoriesByRuleIds(rules.Select(r => r.discountRuleId).ToList())
                : new List<DiscountRuleCategory>();

            return (rules, ruleCategories);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error GetActiveRulesWithCategories: " + ex.Message);
            return (new List<DiscountRule>(), new List<DiscountRuleCategory>());

        }      
    }
}