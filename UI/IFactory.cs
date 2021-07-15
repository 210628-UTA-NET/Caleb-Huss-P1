namespace UI
{
    public interface IFactory
    {
        IMenu GetMenu(MenuType p_menu);
    }
}