local roomNameTrigger = {}

roomNameTrigger.name = "vitellary/roomnametrigger"

roomNameTrigger.placements = {
    {
        name = "room_name_trigger",
        data = {
            roomName = "",
            backgroundColor = "000000FF",
            textColor = "FFFFFFFF",
            disappearTimer = -1,
            oneUse = false,
            instant = false,
        }
    }
}
roomNameTrigger.fieldInformation = {
    backgroundColor = {
        fieldType = "color"
    },
    textColor = {
        fieldType = "color"
    },
}

return roomNameTrigger
