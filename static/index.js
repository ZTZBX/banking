function showMenu(menu) {

    var list_of_menus = ["balance_menu", "deposit_menu", "withdraw_menu", "transfer_menu", "welcome_atm"];
    var list_of_bottons = ["balance_button", "deposit_button", "withdraw_button", "transfer_button", "welcome_atm_botton"];
    for (i in list_of_menus)
    {
        if (list_of_menus[i] != menu){
            document.getElementById(list_of_menus[i]).style.display = "none";
        }
        else {
            
            document.getElementById(list_of_menus[i]).style.display = "block";

            if (menu == "deposit_menu"){
                document.getElementById("depositacction_button").style.display = "block";
            }

            if (menu == "welcome_atm")
            {
                document.getElementById("depositacction_button").style.display = "none";

                for (j in list_of_bottons)
                {
                    if (list_of_bottons[j] == "welcome_atm_botton")
                    {
                        document.getElementById(list_of_bottons[j]).style.display = "none";
                    } 
                    else {
                        document.getElementById(list_of_bottons[j]).style.display = "block";
                    }
                }
            }

            if (menu != "welcome_atm")
            {
                for (j in list_of_bottons)
                {
                    if (list_of_bottons[j] != "welcome_atm_botton")
                    {
                        document.getElementById(list_of_bottons[j]).style.display = "none";
                    } 
                    else {
                        document.getElementById(list_of_bottons[j]).style.display = "block";
                    }
                }
            }

        }
    }
}