param(
  [string]$BaseUrl = 'http://localhost:15672',
  [string]$User = 'admin',
  [string]$Pass = 'admin'
)

$sec = ConvertTo-SecureString $Pass -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential($User, $sec)

function New-RmqExchange($vhost, $name, $type='direct') {
  $body = @{ type=$type; durable=$true; auto_delete=$false; internal=$false; arguments=@{} } | ConvertTo-Json -Compress
  Invoke-RestMethod -Uri "$BaseUrl/api/exchanges/$([uri]::EscapeDataString($vhost))/$name" -Method Put -ContentType 'application/json' -Body $body -Credential $cred | Out-Null
}

function New-RmqQueue($vhost, $name, $args=@{}) {
  $body = @{ durable=$true; auto_delete=$false; arguments=$args } | ConvertTo-Json -Compress
  Invoke-RestMethod -Uri "$BaseUrl/api/queues/$([uri]::EscapeDataString($vhost))/$name" -Method Put -ContentType 'application/json' -Body $body -Credential $cred | Out-Null
}

function New-RmqBinding($vhost, $exchange, $queue, $routingKey) {
  $body = @{ routing_key=$routingKey; arguments=@{} } | ConvertTo-Json -Compress
  Invoke-RestMethod -Uri "$BaseUrl/api/bindings/$([uri]::EscapeDataString($vhost))/e/$exchange/q/$queue" -Method Post -ContentType 'application/json' -Body $body -Credential $cred | Out-Null
}

function Publish-Rmq($vhost, $exchange, $routingKey, $payload) {
  $msg = @{ properties=@{}; routing_key=$routingKey; payload=$payload; payload_encoding='string' } | ConvertTo-Json -Compress
  Invoke-RestMethod -Uri "$BaseUrl/api/exchanges/$([uri]::EscapeDataString($vhost))/$exchange/publish" -Method Post -ContentType 'application/json' -Body $msg -Credential $cred
}

# Ensure vhost
try { Invoke-RestMethod -Uri "$BaseUrl/api/vhosts/%2f" -Credential $cred | Out-Null } catch { Invoke-RestMethod -Uri "$BaseUrl/api/vhosts/%2f" -Method Put -Credential $cred | Out-Null }

# Create DLX and queues
New-RmqExchange '/' 'dlx.nfe' 'direct'
New-RmqQueue '/' 'Modelo55.dlq'

# If Modelo55 exists with different durability, delete then recreate
try { Invoke-RestMethod -Uri "$BaseUrl/api/queues/%2f/Modelo55" -Credential $cred | Out-Null; 
  Invoke-RestMethod -Uri "$BaseUrl/api/queues/%2f/Modelo55" -Method Delete -Credential $cred | Out-Null } catch {}
New-RmqQueue '/' 'Modelo55' @{ 'x-dead-letter-exchange' = 'dlx.nfe'; 'x-dead-letter-routing-key' = 'Modelo55.dlq' }
New-RmqBinding '/' 'dlx.nfe' 'Modelo55.dlq' 'Modelo55.dlq'

# Publish a poison message (should be nacked and dead-lettered)
$poison = 'not-an-xml'
Publish-Rmq '/' '' 'Modelo55' $poison | Out-Null
Write-Host 'DLQ setup complete and poison message published.'
