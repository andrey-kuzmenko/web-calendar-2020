'use strict';

self.addEventListener('push', function (event) {
  console.log('[Service Worker] Push Received.');
  console.log(`[Service Worker] Push had this data: "${event.data.text()}"`);

  const data = event.data.json();
  const title = data.Title;
  const options = {
    body: data.Message,
    data: data.Url
    //icon: 'images/icon.png',
    //badge: 'images/badge.png'
  };

  event.waitUntil(self.registration.showNotification(title, options));
});

self.addEventListener('notificationclick', function (event) {
  const urlToOpen = new URL(event.notification.data, self.location.origin).href;

  event.notification.close();

  event.waitUntil(clients.openWindow(urlToOpen));
});
