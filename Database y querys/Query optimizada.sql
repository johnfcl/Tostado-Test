/*
=========================================================
QUERY: Promedio de días entre Estado "Registrado" y "Validado"
Agrupado por Tipo de documento
Autor: John Fredy Culma Leal
Fecha: 2025-10-25
=========================================================
*/

SELECT
    Tipo,
    COUNT(*) AS TotalDocumentos,
    AVG(DATEDIFF(DAY, FechaRegistro, FechaValidacion)) AS PromedioDiasValidacion
FROM dbo.Documentos WITH (INDEX(IX_Documentos_Estado_Fecha))
WHERE Estado = 'Validado'
GROUP BY Tipo
ORDER BY PromedioDiasValidacion DESC;