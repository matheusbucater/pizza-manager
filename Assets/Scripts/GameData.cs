//===================================================================================================
// Nome: Matheus Pelegrini Bucater
// Data: 17/10/2024
//===================================================================================================
public class GameData
{
    //===============================================================================================
    // Declaração de Variáveis
    //===============================================================================================
    private int    _day;                     // Contador de dias
    private double _money;                   // Quantidade de dinheiro que o player possui
    private int[]  _upgradeLevels;           // Level de cada upgrade do player
    private int    _ingredients;             // Número de ingredientes que o player possui
    private double _moneyCostPerIngredient;  // Preço do ingrediente
    private double _ingredientsCostPerPizza; // Número ingredientes necessários para fazer uma pizza
    private double _customersPerHour;        // Número de clientes que visitam a pizzaria a cada hora
    private double _pizzasPerHour;           // Número de pizzas que podem ser feitas a cada hora
    private double _pizzaPrice;              // Preço de venda da pizza

    //===============================================================================================
    // Construtor
    //===============================================================================================
    public GameData(
        int day, 
        double money, 
        int[] upgradeLevels, 
        int ingredients,
        double moneyCostPerIngredient,
        double ingredientsCostPerPizza, 
        double customersPerHour,
        double pizzasPerHour,
        double pizzaPrice
    ) {

        _day = day;
        _money = money;
        _upgradeLevels = upgradeLevels;
        _ingredients = ingredients;
        _moneyCostPerIngredient = moneyCostPerIngredient;
        _ingredientsCostPerPizza = ingredientsCostPerPizza;
        _customersPerHour = customersPerHour;
        _pizzasPerHour = pizzasPerHour;
        _pizzaPrice = pizzaPrice;
    }

    //===============================================================================================
    // Getters e Setters
    //===============================================================================================
    public int Day {
        get => _day;
        set => _day = value;
    }
    public double Money {
        get => _money;
        set => _money = value;
    }
    public int[] UpgradeLevels {
        get => _upgradeLevels;
        set => _upgradeLevels = value;
    }
    public int Ingredients {
        get => _ingredients;
        set => _ingredients = value;
    }
    public double MoneyCostPerIngredient {
        get => _moneyCostPerIngredient;
        set => _moneyCostPerIngredient = value;
    }
    public double IngredientsCostPerPizza {
        get => _ingredientsCostPerPizza;
        set => _ingredientsCostPerPizza = value;
    }
    public double CustomersPerHour {
        get => _customersPerHour;
        set => _customersPerHour = value;
    }
    public double PizzasPerHour {
        get => _pizzasPerHour;
        set => _pizzasPerHour = value;
    }
    public double PizzaPrice {
        get => _pizzaPrice;
        set => _pizzaPrice = value;
    }
}
