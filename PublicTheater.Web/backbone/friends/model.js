

define([

    'friends/facebook/event',
    'friends/facebook/eventCollection',
    'friends/facebook/friend',
    'friends/facebook/friendCollection',
    'friends/facebook/user',
    'friends/tessitura/performance',
    'friends/tessitura/performanceCollection',
    'friends/tessitura/reservation',
    'friends/tessitura/reservationCollection',
    'friends/tessitura/seat',
    'friends/tessitura/seatCollection'

], function (Event, EventCollection, Friend, FriendCollection, User, Performance, PerformanceCollection, Reservation, ReservationCollection, Seat, SeatCollection) {

    return {
        Event: Event,
        EventCollection: EventCollection,
        Friend: Friend,
        FriendCollection: FriendCollection,
        User: User,
        Performance: Performance,
        PerformanceCollection: PerformanceCollection,
        Reservation: Reservation,
        ReservationCollection: ReservationCollection,
        Seat: Seat,
        SeatCollection: SeatCollection
    };

});