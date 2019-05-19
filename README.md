# api-middleware-exception-handler

Este pequeño proyecto contiene código para un middleware de net core que captura las excepciones que se generan dentro de una servicio. 

Es posible configurar para cada excepción en particular, un mensaje, un código de error y el http status code. De esta forma gestionar de forma granular la respuesta a los clientes que consumen el servicio retornando un mensaje que permita entender si el error es un problema técnico interno o está relacionado a una condición de negocio.
