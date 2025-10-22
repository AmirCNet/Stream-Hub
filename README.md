# StreamHub: API REST de Streaming

## Objetivo

Desarrollar una API RESTful para un servicio de streaming (denominado **"StreamHub"**)  
que administre el contenido disponible (películas, series), gestione las suscripciones  
de los usuarios y controle el acceso a dicho contenido basándose en el estado de pago  
de la suscripción, consultando una API externa de pasarela de pagos.

---

## Requerimientos Funcionales

La API debe exponer endpoints claros y seguros, utilizando los métodos HTTP estándar  
(CRUD).

### 1. Gestión de Contenido

Implementar la lógica CRUD para administrar el catálogo de contenido.

**Datos del Contenido:**  
- ID  
- Título  
- Descripción  
- Tipo (Película/Serie)  
- Género  
- Clasificación de Edad  
- URL del stream  

**Listado:** Un endpoint para listar el catálogo completo.  
**Detalle:** Un endpoint para obtener la información detallada de una pieza de contenido por su ID.

---

### 2. Gestión de Usuarios y Suscripciones

Implementar la lógica para registrar usuarios y gestionar su plan de suscripción.

**Datos del Usuario:**  
- ID  
- Nombre  
- Email  
- Contraseña (simulada o hash)  
- Fecha de Registro  

**Datos de la Suscripción:**  
- ID  
- ID del Usuario  
- Tipo de Plan (ej. Básico, Premium)  
- Fecha de Inicio del Período de Facturación (Mensual)  
- Estado de la Suscripción (ej. Activa, Cancelada, Pendiente de Pago)  

**Funcionalidad:**  
- **Crear Suscripción:** Asignar un plan a un nuevo usuario, estableciendo la fecha de inicio del período de facturación actual.

---

### 3. Control de Acceso al Contenido

El requisito principal del negocio: un usuario solo puede acceder al contenido si su  
suscripción está en estado **"Activa"** (pago al día).

**Endpoint de Acceso:**  
`GET /content/{content_id}/play`

Debe:

1. Verificar la autenticación del usuario (ID de usuario).  
2. Verificar el estado de su suscripción.  
3. Si el pago está pendiente o la suscripción cancelada, debe denegar el acceso y devolver un código de error apropiado (ej. `403 Forbidden`).  
4. Si el pago está activo, debe permitir el acceso (ej. devolviendo la URL del stream).

---

## Requerimientos de Integración con Pasarela de Pagos

El sistema depende de una API externa para validar la vigencia de la suscripción.

### 4. Administración de la Pasarela de Pagos

El sistema debe almacenar y utilizar la configuración del sistema de pagos.

**Datos de la Pasarela:**  
- ID  
- Nombre  
- URL base de su API para consulta de pagos  
- API Key o Credenciales de acceso (simuladas)

---

### 5. Control de Pagos y Estado de la Suscripción

Implementar un mecanismo que utilice la pasarela externa para mantener el estado de la  
suscripción actualizado.

**Función de Verificación de Pago:**  
Crear una función (que podría ser invocada por un proceso batch diario o por un endpoint de conciliación) que haga lo siguiente para cada usuario con una suscripción:

1. Calcule el **Código de Pago Único** que debería tener la boleta de ese mes para el usuario (simulado).  
2. Consulte la **API de la Pasarela de Pagos** (utilizando la URL y credenciales del punto 4) enviando el Código de Pago Único.  
3. Actualice el **Estado de la Suscripción:**  
   - Si la Pasarela confirma el pago, el estado se mantiene o se establece a **"Activa"**.  
   - Si la Pasarela no confirma el pago (y el período de facturación ya venció), el estado se establece a **"Pendiente de Pago"**.

---

## Consideraciones Técnicas (Opcional, pero recomendado)

- Implementar un sistema de **Autenticación/Autorización** (simulando tokens JWT o API Keys) para proteger los endpoints sensibles.  
- Manejar el concepto de **Período de Facturación** para saber exactamente qué mes debe consultar en la Pasarela de Pagos.

---

## Entregables

- El **código fuente** de la API RESTful.  
- El **modelo de la base de datos** para Contenido, Usuarios y Suscripciones.  
- **Documentación** de los endpoints principales, con especial énfasis en el flujo de Control de Acceso.

---

## ¡Desafío!

Implementar un endpoint que permita a los usuarios **cancelar su suscripción**,  
estableciendo el estado a **"Cancelada"**, pero permitiendo el acceso al contenido hasta el  
final del período de facturación ya pagado.
