var cloakspan = document.getElementById("cloakCounter");
var stonespan = document.getElementById("stoneCounter");
var wandspan = document.getElementById("wandCounter");

var connectionHallows = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/hallowshub").build();

connectionHallows.on("UpdateHallowCount", (cloak, stone, wand) => {
    cloakspan.innerText = cloak.toString();
    stonespan.innerText = stone.toString();
    wandspan.innerText = wand.toString();
});


function fulfilled() {
    connectionHallows.invoke("GetRaceStatus").then((raceCounter) => {
        cloakspan.innerText = raceCounter.cloak.toString();
        stonespan.innerText = raceCounter.stone.toString();
        wandspan.innerText = raceCounter.wand.toString();
    }
    )
    console.log("User count connection successful");
    
}

function rejected(err) {
    console.error(err.toString());
}

connectionHallows.start().then(fulfilled, rejected);
