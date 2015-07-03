require  'dipsclient'


client = DiPS::DiPSClient.new "ws://192.168.56.101:8888/dips"
puts "Connecting"
sleep 2.0
#k = gets.chomp
client.Subscribe ("wordsearch") { |m| 

	word = m["Word"]
	allfound = Array.new
	texts = '/home/pedro/Projects/books/ruby/'
	#search in all the files of certain folder

	Dir.foreach(texts) do |item|

	  next if item == '.' or item == '..'
	  i = 1

	  File.open("#{texts}#{item}", "r") do |f|
		f.each_line do |line|
			i += 1
			#search the word in the file
			line = line.to_s.delete('\n')
			if line.include? word
				found = { "BookName" => item, "Line" => line, "LineNumber" => i, "Word" => word }
				allfound << found
			end
			
		end
	  end
  
	end

	#publish the result
	client.Publish "searchresults", allfound
}
#param = {"name"=>"pedro", "age" =>39}
#puts "Publishing"
#client.Publish "test", param
puts "Enter to exit"
k = gets.chomp

