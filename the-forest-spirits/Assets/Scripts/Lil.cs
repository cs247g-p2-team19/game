/*
 * A lil' class with lil' shortcuts to
 * easier to access various functionality
 */

public class Lil
{

    public static Player Guy => Player.Instance;
    public static Player Gal => Player.Instance;
    public static Player Pal => Player.Instance;



    public static Inventory Inventory => Player.Instance.inventory;
    public static Inventory Pockets => Player.Instance.inventory;
}