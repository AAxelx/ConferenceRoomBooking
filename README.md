# ConferenceRoomBooking
Technical task: https://drive.google.com/file/d/1nkY9mT3Do3EyGLodsDbdeNrHPpFZuf3e/view?usp=sharing

## Project Overview

**ConferenceRoomBooking** is an API for managing the booking and rental of conference rooms. The application allows users to find available rooms, book them for specific dates and times, and select additional services.

## Key Features

- Manage conference room bookings
- Search for available rooms by date and time
- Support for additional services with bookings
- Calculate rental costs based on time and selected services
- User authentication and access management

## API Endpoints

The project exposes several endpoints for interacting with the booking system:

- **Rooms**
  - `GET /api/room/available` — Retrieve a list of available conference rooms
  - `POST /api/room` — Create a new conference room
  - `PUT /api/room` — Update room information
  - `DELETE /api/room/{id}` — Delete a room

- **Bookings**
  - `POST /api/booking` — Create a new booking

- **User**
  - `POST /api/service` — Add a new service

## Project Structure

- **API** — Controllers for room and booking management
- **Common** — Shared models and DTOs
- **DAL** — Data Access Layer, repositories
- **Services** — Business logic

## Testing

Unit tests have been implemented for the core algorithm responsible for calculating the total booking cost, considering factors like base price, duration, and additional selected services.
