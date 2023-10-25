local coyoteBounceTrigger = {}

coyoteBounceTrigger.name = "vitellary/coyotebounce"

coyoteBounceTrigger.fieldInformation = {
    time = {
        minimumValue = 0.0
    },
    directions = {
        options = {"Top", "TopAndSides", "AllDirections"},
        editable = false
    }
}

coyoteBounceTrigger.placements = {
    {
        name = "coyote_bounce",
        data = {
            directions = "Top",
            time = 0.1,
            refill = true,
            setGrounded = false,
        }
    }
}

return coyoteBounceTrigger
