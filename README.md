# Airplane Parking Assistant

An API to manage airplane parking at an airport. Exposes an endpoint for recommending the best available slot for a plane at a given time, for a given duration, and another endpoint for reserving a plane in a slot, at a given time, for a given duration.

The recommended slots comes from the Slot Service, which in turn invokes a Slot Score Provider to score the appropriateness of the available slots for the given airplane. The first slot with the highest score is returned as the recommendation. This pattern is extensible in that additional Score Providers can be implemented to score the slots against other criteria, and with some additional additional refactoring and aggregation, the scores could be based on other criteria, such as distance to taxi, preferred terminal etc.
