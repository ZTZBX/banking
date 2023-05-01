function showMenu(menu) {

    var list_of_menus = ["balance_menu", "deposit_menu", "withdraw_menu", "transfer_menu", "welcome_atm"];
    var list_of_bottons = ["balance_button", "deposit_button", "withdraw_button", "transfer_button", "welcome_atm_botton", "exit_botton"];
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

            if (menu == "withdraw_menu"){
                document.getElementById("withdrawacction_button").style.display = "block";
            }

            if (menu == "welcome_atm")
            {
                document.getElementById("depositacction_button").style.display = "none";
                document.getElementById("withdrawacction_button").style.display = "none";

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


function changeMoney(data) {
    
    data = JSON.parse(data["data"])
    document.getElementById("moneyAccount").innerText = data.money
    document.getElementById("vpAccount").innerText = data.vp
    document.getElementById("currencyAccount").innerText = data.currency
}

$(function () {
    window.addEventListener('message', function (event) {
        var item = event.data;

        if (item.showAtm == true) {

            fetch(`https://banking/getMoney`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=UTF-8',
                },
                body: JSON.stringify({
                })
            }).then(resp => resp.json()).then(success => changeMoney(success));

            document.getElementsByClassName("main")[0].style.display = "block";

        }
        else {
            document.getElementsByClassName("main")[0].style.display = "none";
        }
    });

    $("#exit_botton").click(function () {
  
            fetch(`https://banking/exit`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=UTF-8',
            },
        }).then()
        .catch(err => {   
        });
    });

});