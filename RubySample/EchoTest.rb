require 'dipsclient'


client = DiPS::DiPSClient.new "ws://localhost:8888/dips"
puts "Connecting"
sleep 2.0
puts "Subscribing"
client.Subscribe ("test") { |payload| puts "Received from DiPS: #{payload}" }
param = {"name"=>"pedro", "age" =>39}
puts "Publishing"
client.Publish "test", param
puts "Enter to exit"
k = gets.chomp

